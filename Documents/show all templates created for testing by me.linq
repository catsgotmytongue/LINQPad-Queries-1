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
	
	//templates
	var templates = JjisDocumentTemplateDefinitions
			.Where(t => t.TemplateDescription.StartsWith("Curt") && t.TemplateDescription.Contains("_Do_Not_Use"))
			.ToList();

	templates.Dump();
	//template ids of the templates created already	
	var templateIds = templates.Select(t => t.JjisDocTemplateDefinitionId).ToList();
	templateIds.Dump();

	//master docs
	JjisDocumentDefinitions.Where(jdd => jdd.DocumentDescription.StartsWith("AAA_Curt")).Dump();
	
	var docDetails = JjisDocumentDetails.Where(s=>templateIds.Contains( s.JjisDocumentTemplateDefnId.GetValueOrDefault() ));
	docDetails.Dump();
}

// Define other methods and classes here
