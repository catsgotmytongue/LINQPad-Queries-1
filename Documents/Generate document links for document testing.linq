<Query Kind="Program">
  <Connection>
    <ID>836967e7-c6a0-40c1-ab6d-15d75f502367</ID>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://cenvapprd38.oya.ad/CoreDataService/odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	//UpdateTemplate("DotNet_GeneralFaceSheetTest_Do_Not_Use", @"\\cenvfsprd1\home\mullinc\General Facesheet-sample-document-template.docx");

	
	var tokenString = "c665d82e-09e9-41d4-ae30-08e66448a887";
	var masterDocumentId = 701021107048;
	var youthId = 139718439605;
	var docType = "YTH";
	
	var server = "jjisweb-dev.oya.state.or.us";
	//var server = "localhost:44300";
	//var server = "localhost.fiddler:44300";

	var editUrl = $@"https://{server}/documents/template/{tokenString}/edit";

	var createTemplateName = "CurtWordTemplate1_Do_Not_Use.docx";

	var createUrl = $@"https://{server}/documents/template/{tokenString}/create/{docType}/{youthId}/{createTemplateName}";

	
	var dotNetTemplates = JjisDocumentTemplateDefinitions.FilterforDotNetTemplates()
	.Select(template => new
	{
		TemplateName = template.TemplateDescription,
		EditUrl = $@"{WordCommands.OpenForViewInWord}{editUrl}/{template.JjisDocTemplateDefinitionId}/{template.TemplateDescription}.docx"
	}).ToList();
	
	var sb = new StringBuilder($"<h3>Documents on {server}</h3>");

	sb.Append($"\n<h2>Create</h2>\n<a href=\"{createUrl}\">Create {createTemplateName} </a>");
	sb.Append($"\n<h2>Edit</h2>");
	sb.Append("\n<ul>");

	foreach (var dotNetTemplate in dotNetTemplates)
	{
		sb.Append($"\n\t<li>\n\t\t<a href=\"{dotNetTemplate.EditUrl}\">\n\t\t\tEdit {dotNetTemplate.TemplateName}\n\t\t</a>\n\t</li>");
	}
	
	sb.Append("\n</ul>");

	sb.Append($"<h2>Generate from master document</h2>");
	var generateUrl = $"ms-word:ofe|u|https://{server}/documents/document/{tokenString}/create/{masterDocumentId}/YTH/139718439605/WordTest_Do_Not_Use_From_JJIS.docx";
	sb.Append($"<a href=\"{generateUrl}\">Generate From Master AAA_Curt </a>");
	
	Console.WriteLine(sb.ToString());
	
	//dotNetTemplates.Dump();
	//createUrl.Dump();
	
}

// update the file in the database identified by the title/templateDescription with the binary found at localFilename
void UpdateTemplate(string documentTitle, string localFilename)
{
	var template1 = JjisDocumentTemplateDefinitions
				.Where(jdtd => jdtd.TemplateDescription == documentTitle)
				.FirstOrDefault();
				
	template1.BinaryDocument = File.ReadAllBytes(localFilename);
	
	UpdateObject(template1);
	SaveChanges();
}


// Define other methods and classes here
public static class DataServiceExtensions
{
	public static IQueryable<JjisDocumentTemplateDefinition> FilterforDotNetTemplates(this IQueryable<JjisDocumentTemplateDefinition> documentTemplateDefs)
	{
		return documentTemplateDefs.Where(jdtd => jdtd.TemplateDescription.StartsWith("Curt") && jdtd.TemplateDescription.EndsWith("_Do_Not_Use"));
	}
}

public static class WordCommands
{
	public const string OpenForEditInWord = "ms-word:ofe|u|";
	public const string OpenForViewInWord = "ms-word:ofv|u|";
}