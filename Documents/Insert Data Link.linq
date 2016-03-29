<Query Kind="Statements">
  <Connection>
    <ID>836967e7-c6a0-40c1-ab6d-15d75f502367</ID>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://cenvapprd38.oya.ad/CoreDataService/odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
</Query>

var id = JjisNetDataLinks.ToList().Max(jndl => jndl.DataLinkId) + 1;

var dataLink = new JjisNetDataLink
{
	DataLinkId = id,
	Name = "Youth Object",
	Identifier = "Youth_SomeObject",
	Expression = "Youth_ObjectExpression()",
	InstanceFlagAsString = "N",
	ReturnTypeCode = "O",
	Description = "testYouthObject",
	EntityClassName = "Youth"
};

AddObject("JjisNetDataLinks", dataLink);
SaveChanges();