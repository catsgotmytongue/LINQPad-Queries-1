<Query Kind="Statements">
  <Connection>
    <ID>a3839723-1f14-4b0a-8d14-93952fb67c4f</ID>
    <Persist>true</Persist>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://services-dev.jjis.oya.ad/CoreDataService/Odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
</Query>

Directory.GetCurrentDirectory().Dump();
JjisDocumentDefinitions.Where(j => j.JjisDocumentDefinitionId == 701026544073).Dump();
JjisDocumentDefinitions.Where(j => j.JjisDocumentDefinitionId == 701026598957).Dump();
var i = 0;              

JjisDocumentDetailDefinitions.Where(d=>d.JjisDocumentDefinitionId == 701026598957).Dump();
JjisDocumentTemplateDefinitions.ToList().Where(d => (new long[] { 701026310102}).Contains(d.JjisDocTemplateDefinitionId)).Dump();
JjisDocumentTemplateDefinitions.ToList().Where(d=>d.SourceTypeCode != null && d.LastChangedDate.Value.Date == DateTime.Today.Date)
//.Where(d => d.TemplateDescription.Contains("OYA 3024")).Dump();  defid = 701026732025; docTemplateDefId = 701026698649
//.ToList()
//.Where(d => d.LastChangedDate.Value.Date == DateTime.Today.Date)
//.Select(d => { 
//	System.IO.File.WriteAllBytes($@"c:\temp\badfile_{d.TemplateDescription}.{i++}.docx", d.BinaryDocument); 
//	return true;
//});
//.Dump();
//.Where(d=>d.SourceTemplateDefinitionId == 700118552580).Dump();
//701026310102,701026530568