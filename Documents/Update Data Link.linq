<Query Kind="Statements">
  <Connection>
    <ID>836967e7-c6a0-40c1-ab6d-15d75f502367</ID>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://cenvapprd38.oya.ad/CoreDataService/odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
  <NuGetReference>FileHelpers</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

var dataLink = JjisNetDataLinks.Where(jdl => jdl.DataLinkId == 2).Take(1).FirstOrDefault();
dataLink.ExpandPath = "Referrals/Allegations/AllegationAssociates/Associate/Entity";

UpdateObject(dataLink);
SaveChanges();