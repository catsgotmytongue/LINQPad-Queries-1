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

var id = JjisNetDataLinks.ToList().Max(jndl => jndl.DataLinkId) + 1;

var dataLink = new JjisNetDataLink
{
	DataLinkId = id,
	Name = "Youth Location Close Reason",
	Identifier = "YouthLocation_CloseReason",
	Expression = "youthLocation.EndReason",
	ReturnTypeCode = "P",
	Description = "The Youth Location Close Reason",
	ParentCategory = "Youth Location",
	VisibleFlag = true,
	EntityClassName = "YouthLocation"
};

AddToJjisNetDataLinks(dataLink);
SaveChanges();