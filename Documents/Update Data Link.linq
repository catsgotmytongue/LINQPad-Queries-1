<Query Kind="Statements">
  <Connection>
    <ID>fa581a87-8c8a-4533-8811-093473d27ab8</ID>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://services-dev.jjis.oya.ad/CoreDataService/odata</Uri>
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