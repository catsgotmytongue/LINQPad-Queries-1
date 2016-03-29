<Query Kind="Statements">
  <Connection>
    <ID>836967e7-c6a0-40c1-ab6d-15d75f502367</ID>
    <Persist>true</Persist>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://cenvapprd38.oya.ad/CoreDataService/odata</Uri>
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