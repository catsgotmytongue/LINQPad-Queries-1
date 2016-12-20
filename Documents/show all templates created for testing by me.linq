<Query Kind="Program">
  <Connection>
    <ID>fa581a87-8c8a-4533-8811-093473d27ab8</ID>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://services-dev.jjis.oya.ad/CoreDataService/odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	
	//templates
	var templates = JjisDocumentTemplateDefinitions
			.Where(t => /*t.TemplateDescription.StartsWith("Curt") &&*/ t.TemplateDescription.Contains("_Do_Not_Use"))
			.ToList();

	templates.Dump();
	//template ids of the templates created already	
	var templateIds = templates.Select(t => t.JjisDocTemplateDefinitionId).ToList();
	templateIds.Dump();

	//master docs
	JjisDocumentDefinitions.Where(jdd => jdd/*.DocumentDescription.StartsWith("AAA_Curt")*/).Dump();
	
	var docDetails = JjisDocumentDetails.Where(s=>templateIds.Contains( s.JjisDocumentTemplateDefnId.GetValueOrDefault() ));
	docDetails.Dump();
}

// Define other methods and classes here