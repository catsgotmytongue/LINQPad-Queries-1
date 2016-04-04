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

		var topLevelControls = new List<dynamic>();
		foreach (var cc in allTopLevelControls)
		{
			topLevelControls.Add(GetJsonTree(cc, _jobj.Root, "  "));
		}
		
		

	}
	Console.WriteLine($"\nThe JSON:\n{_jobj.ToString()}");
}


dynamic GetJsonTree(SdtElement theSdtElement, JToken currentToken, string tab)
{
	if(currentToken == null)
		return null;
		
	// get all the <w:sdtContent> Elements inside theSdtElement (could be runs or blocks)
	var sdtContentElements = theSdtElement.Elements().Where(e=>e is SdtContentRun || e is SdtContentBlock);

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

	Console.WriteLine($"{tab}currentToken({sdtId}): {currentToken?.Type}");

	// <w:alias w:val="{Val}">
	var sdtAlias = sdtPropertyElement.SdtAlias()?.Val;
	Console.WriteLine($"{tab}SdtAlias:{sdtAlias}; " +
						$"SdtId:{sdtId}; IsRepeatingSection: {isRepeatingSection}; " +
						$"IsRepeatingSectionItem: {isRepeatingSectionItem}; " +
						$"Object: {sdtIsObject}; Array: {sdtIsArray};");

	//Console.WriteLine($"{String.Join(", ",theSdtElement.Elements().Select(s=>s.LocalName))}");
	

	var currentContainer = (JContainer)currentToken;
	var tagStr = tag?.Val?.Value;
	var objectOrCollectionName = tagStr?.Substring(0, tagStr.Length - 2);

	// new property to add to our json object
	var newProperty = sdtIsArray ? new JProperty(objectOrCollectionName??"NONAME", new JArray()) : new JProperty(objectOrCollectionName??"NONAME", new JObject());
	var tree = new JObject();
	
	// foreach of theSdtElement's childContentElements
	foreach (OpenXmlElement contentElement in sdtContentElements)
	{
		if (contentElement is SdtContentRun)
		{
			// add the run to the set of children for this node
			currentContainer.Add(newProperty);
        }
		else if (contentElement is SdtContentBlock)
		{
			// can contain other elements so here is the place to recurse down
			foreach (var sdtElement in contentElement.Elements<SdtElement>())
			{
				var t = GetJsonTree(sdtElement, currentToken?.SelectToken($".{objectOrCollectionName}"), tab+tab);
				
			}
		}
		else
		{
			// whatever it is if not a run or a block?
		}
	}
	
	// do something at the sdt level
	//if(theSdtElement)
	
	return tree;
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

	public static IEnumerable<SdtElement> SdtElements(this SdtContentBlock sdtContentBlock)
	{
		return sdtContentBlock.Elements<SdtElement>();
	}

}