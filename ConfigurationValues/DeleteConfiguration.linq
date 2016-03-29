<Query Kind="Statements">
  <Connection>
    <ID>a33ccdf6-b147-48fb-84e0-2ce107b3ffbd</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>https://cenvapprd38.oya.ad/CoreDataService/CoreDataService.svc/</Server>
  </Connection>
</Query>

DeleteObject(ConfigurationValues.Where(cv => cv.ConfigurationValueId == 0).Take(1).ToList().First());
SaveChanges();