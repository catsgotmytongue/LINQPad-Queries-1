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

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="WebTokenEndpoint",
        Value="/oauth2/token" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="WebappId",
        Value="6ac4d09f767948ce8d7af5317cbcda46" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="WebappKey",
        Value="U1hoeVFXcEViMkV5Um5GRmJFODNTV2h5VTNKVlNrVk1hRlZqYTJWUVJWQldjR0Zs" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="WebTokenExpireTime",
        Value="00:03:00" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="WebappRefreshTokenAudience",
        Value="bba2b21dd7764b279c918177a99d92f7" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="WebappRefreshTokenKey",
        Value="YTE5Y2ZmNWVhM2EwNDY2MWI4YTM1ZGJkYmU4OGE1OTMyYzliYzM2NjAxN2E0ZDNi" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="SmallTalkTokenEndpoint",
        Value="/oauth2/sttoken" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="SmallTalkId",
        Value="a68cd739eefa4c0dabcb364a7d22f909" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="SmallTalkKey",
        Value="ZThkNTA0ODFjODIwMjkzMzMwYTZkNGY2NTQ0ZjU0YWY3YTk1MGU2YTRjN2JkN2Mw" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="SmallTalkTokenExpireTime",
        Value="10:00:00" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="DocumentsTokenEndpoint",
        Value="/oauth2/doctoken" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="DocumentsId",
        Value="56c4f181062442d9b1cced0c5e18c2fe" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="DocumentsKey",
        Value="MDRlYmI4ODk1ZjRiYzdiNzBhNzI2NWRjZTMzYjBhYjk0NGM1MTg3ZmJlZmI4OGJl" 
});

AddToConfigurationValues(new ConfigurationValue
{ 
        ConfigurationValueId = newConfigId++,
        ApplicationName = "JJIS.Web",
        Key="DocumentsTokenExpireTime",
        Value="10:00:00" 
});


SaveChanges();

ConfigurationValues.OrderByDescending(cv => cv.ConfigurationValueId).Dump();

//DeleteObject(ConfigurationValues.Where(cv => cv.ConfigurationValueId == 0).Take(1).ToList().First());
//SaveChanges();