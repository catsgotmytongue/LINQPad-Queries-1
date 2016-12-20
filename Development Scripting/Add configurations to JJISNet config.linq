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