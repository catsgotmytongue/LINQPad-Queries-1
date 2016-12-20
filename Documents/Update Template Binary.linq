<Query Kind="Statements">
  <Connection>
    <ID>fa581a87-8c8a-4533-8811-093473d27ab8</ID>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://services-dev.jjis.oya.ad/CoreDataService/odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
</Query>

var template1 = JjisDocumentTemplateDefinitions
				.Where(jdtd => jdtd.TemplateDescription == "WordTemplate1_Do_Not_Use")
				.FirstOrDefault();

var template2 = JjisDocumentTemplateDefinitions
				.Where(jdtd => jdtd.TemplateDescription == "WordTemplate2_Do_Not_Use")
				.FirstOrDefault();

var template3 = JjisDocumentTemplateDefinitions
				.Where(jdtd => jdtd.TemplateDescription == "WordTemplate3_Do_Not_Use")
				.FirstOrDefault();

template1.BinaryDocument = File.ReadAllBytes(@"C:\Temp\WordTemplate1.docx"); ;
template2.BinaryDocument = File.ReadAllBytes(@"C:\Temp\WordTemplate2.docx"); ;
template3.BinaryDocument = File.ReadAllBytes(@"C:\Temp\WordTemplate3.docx"); ;

UpdateObject(template1);
UpdateObject(template2);
UpdateObject(template3);
SaveChanges();