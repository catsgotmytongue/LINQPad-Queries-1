<Query Kind="Statements">
  <Connection>
    <ID>a33ccdf6-b147-48fb-84e0-2ce107b3ffbd</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>https://cenvapprd38.oya.ad/CoreDataService/CoreDataService.svc/</Server>
  </Connection>
</Query>

var maxConfig = ConfigurationValues
	.OrderByDescending(cv => cv.ConfigurationValueId)
	.Take(1)
	.Select(cv => new { cv.ConfigurationValueId} )
	.ToList()
	.FirstOrDefault();
	
	
var newConfigId = maxConfig != null ? maxConfig.ConfigurationValueId + 1 : 1;

AddToConfigurationValues(new ConfigurationValue
{
	ConfigurationValueId = newConfigId,
	ApplicationName = "CoreSecurityService",
	Key = "AuthenticationLockoutDuration",
	Value = "30"
});

SaveChanges();

ConfigurationValues.OrderByDescending(cv => cv.ConfigurationValueId).Dump();

//DeleteObject(ConfigurationValues.Where(cv => cv.ConfigurationValueId == 0).Take(1).ToList().First());
//SaveChanges();