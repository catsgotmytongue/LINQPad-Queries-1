<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>State.OR.Oya.Core.Utility</NuGetReference>
  <NuGetReference>DocumentFormat.OpenXml</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>DocumentFormat.OpenXml</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.Word</Namespace>
  <Namespace>DocumentFormat.OpenXml.Packaging</Namespace>
  <Namespace>DocumentFormat.OpenXml.Wordprocessing</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Caching</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Diagnostics</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.IO</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Logging</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Reflection</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.ServiceClient</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.String</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Threading</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Xml</Namespace>
</Query>

private JObject _jobj = new JObject();

void Main()
{
	using (WordprocessingDocument doc = WordprocessingDocument.Open(@"c:\temp\nestedcontentcontrols5.docx", false))
	{
		// get the sdt elements at the top level of the tree
		var allTopLevelControls = doc.MainDocumentPart.TopLevelContentControls();

		System.Dynamic.ExpandoObject contentControlTree;
		foreach (var cc in allTopLevelControls)
		{
			HandleChildControls(cc, _jobj.Root, "  ");
		}
	}

	Console.WriteLine($"\nThe JSON:\n{_jobj.ToString()}");
}


void HandleChildControls(SdtElement theSdtElement, JToken currentToken, string tab)
{
	
	// get all the <w:sdtContent> Elements inside theSdtElement
	var sdtContentBlocks = theSdtElement.SdtContentBlocks();

	var isRepeatingSection = theSdtElement.IsRepeatingSection();
	var isRepeatingSectionItem = theSdtElement.IsRepeatingSectionItem();

	// <w:sdtPr> element
	var sdtPropertyElement = theSdtElement.SdtProperties();

	// we will use tag for the metadata needed for individual pieces that make up our template
	var tag = sdtPropertyElement.Tag();

	var sdtIsObject = (tag?.Val?.Value?.EndsWith("{}")).GetValueOrDefault();
	var sdtIsArray = (tag?.Val?.Value?.EndsWith("[]")).GetValueOrDefault();

	// <w:id w:val="{Val}">
	var sdtId = sdtPropertyElement.SdtId().Val;

	//Console.WriteLine($"{tab}currentToken({sdtId}): {currentToken?.Type}");
	
	// <w:alias w:val="{Val}">
	var sdtAlias = sdtPropertyElement.SdtAlias()?.Val;
	Console.WriteLine($"{tab}SdtAlias:{sdtAlias}; " +
						$"SdtId:{sdtId}; IsRepeatingSection: {isRepeatingSection}; " +
						$"IsRepeatingSectionItem: {isRepeatingSectionItem}; " +
						$"Object: {sdtIsObject}; Array: {sdtIsArray};");

	//	if we have an object or collection - we'll represent it with a 'repeating section' SdtElement from office 2013
	if (theSdtElement.IsRepeatingSection())
	{
		var tagStr = tag.Val.Value;
		var objectOrCollectionName = tagStr.Substring(0, tagStr.Length - 2);

		// new property to add to our json object
		var newProperty = sdtIsArray ? new JProperty(objectOrCollectionName, new JArray()) : new JProperty(objectOrCollectionName, new JObject());
		//var arrayIndexer = isArray? "[0]" : String.Empty;
		
		// before we add we must determine what JContainer we're talking about
		switch (currentToken.Type)
		{
			case JTokenType.Object:
				var currentObject = (JObject)currentToken;

				currentObject.Add(newProperty);
				break;
			case JTokenType.Array:
				var currentArray = (JArray)currentToken;
				var newObject = new JObject();

				currentArray.Add(newObject);
				// jump to the newly created JContainer object
				

				break;
		}
		currentToken = currentToken.SelectToken($".{objectOrCollectionName}");
	}
	else
	{
		// non repeating element
		var arrayElementToken = currentToken.SelectToken($"[0]") as JObject;
		if (arrayElementToken != null)
		{
			
		}
		// get the tag and add a property to either the array or the property
		//t.Add(
	}

	// do something with each <w:sdtContent>
	foreach (var sdtContentBlk in sdtContentBlocks)
	{
		// do something with each <w:sdt> element inside the current <w:sdtContent> element
		var sdtElements = sdtContentBlk.Elements<SdtElement>();

		foreach (var sdtElement in sdtElements)
		{
			// handle each <w:sdt> element
			HandleChildControls(sdtElement, currentToken, tab + tab);
		}

	}
}

// Define other methods and classes here
public static class ContentControlExtensions
{
	public static IEnumerable<SdtElement> TopLevelContentControls(this OpenXmlPart part)
	{
		return part.RootElement.Descendants<SdtElement>().Where(sdt => !sdt.Ancestors<SdtElement>().Any());
	}

	public static IEnumerable<OpenXmlElement> ContentControls(
		this OpenXmlPart part)
	{
		return part.RootElement.Descendants<SdtElement>();
	}

	public static IEnumerable<OpenXmlElement> ContentControls(
		this WordprocessingDocument doc)
	{
		foreach (var cc in doc.MainDocumentPart.ContentControls())
			yield return cc;
		foreach (var header in doc.MainDocumentPart.HeaderParts)
			foreach (var cc in header.ContentControls())
				yield return cc;
		foreach (var footer in doc.MainDocumentPart.FooterParts)
			foreach (var cc in footer.ContentControls())
				yield return cc;
		if (doc.MainDocumentPart.FootnotesPart != null)
			foreach (var cc in doc.MainDocumentPart.FootnotesPart.ContentControls())
				yield return cc;
		if (doc.MainDocumentPart.EndnotesPart != null)
			foreach (var cc in doc.MainDocumentPart.EndnotesPart.ContentControls())
				yield return cc;
	}

	public static SdtProperties SdtProperties(this SdtElement sdtElement)
	{
		return sdtElement.Elements<SdtProperties>().First();
	}

	public static SdtId SdtId(this SdtProperties sdtPropertyElement)
	{
		return sdtPropertyElement.Elements<SdtId>().First();
	}

	public static SdtAlias SdtAlias(this SdtProperties sdtPropertyElement)
	{
		return sdtPropertyElement.Elements<SdtAlias>().FirstOrDefault();
	}

	public static Tag Tag(this SdtProperties sdtPropertyElement)
	{
		return sdtPropertyElement.Elements<Tag>().FirstOrDefault();
	}

	public static IEnumerable<SdtContentBlock> SdtContentBlocks(this SdtElement sdtElement)
	{
		return sdtElement.Elements<SdtContentBlock>();
	}

	public static bool IsRepeatingSection(this SdtElement sdtElement)
	{
		return sdtElement.SdtProperties().Elements<SdtRepeatedSection>().Any();
	}
	public static bool IsRepeatingSectionItem(this SdtElement sdtElement)
	{
		return sdtElement.SdtProperties().Elements<SdtRepeatedSectionItem>().Any();
	}

}