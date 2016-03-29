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

var maxConfig = ConfigurationValues
	.OrderByDescending(cv => cv.ConfigurationValueId)
	.Take(1)
	.Select(cv => new { cv.ConfigurationValueId} )
	.ToList()
	.FirstOrDefault();
	
	
var newConfigId = maxConfig != null ? maxConfig.ConfigurationValueId + 1 : 1;

var configs = new Tuple<string, string, string>[] {
	new Tuple<string,string,string>("JJIS.API","TokenIssuer","DEV-123"),
};

foreach (var tuple in configs)
{
	AddToConfigurationValues(new ConfigurationValue
	{ 
	        ConfigurationValueId = newConfigId++,
	        ApplicationName = tuple.Item1,
	        Key = tuple.Item2,
	        Value = tuple.Item3
	});
}

SaveChanges();

ConfigurationValues.OrderByDescending(cv => cv.ConfigurationValueId).Dump();


//DeleteObject(ConfigurationValues.Where(cv => cv.ConfigurationValueId == 0).Take(1).ToList().First());
//SaveChanges();