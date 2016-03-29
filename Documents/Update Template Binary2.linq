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

	
	var tokenString = "e8fc7396-d8be-4241-9c63-26fbb4db5cfe";
	var editUrl = $@"https://jjisweb-dev.oya.state.or.us/documents/davtemplate/{tokenString}/edit/";
 
	var dotNetTemplates = JjisDocumentTemplateDefinitions.FilterforDotNetTemplates()
	.Select(template => new
	{
		DotNetUrl = $@"{WordCommands.OpenForViewInWord}{editUrl}/{template.JjisDocTemplateDefinitionId}/template.docx",
		//DocumentDefinition = template
	}).ToList();
	
	dotNetTemplates.Dump();
	
	
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
		return documentTemplateDefs.Where(jdtd => jdtd.TemplateDescription.EndsWith("_Do_Not_Use"));
	}
}

public static class WordCommands
{
	public const string OpenForEditInWord = "ms-word:ofe|u|";
	public const string OpenForViewInWord = "ms-word:ofv|u|";
}