<Query Kind="Statements">
  <Connection>
    <ID>a3839723-1f14-4b0a-8d14-93952fb67c4f</ID>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://services-dev.jjis.oya.ad/CoreDataService/Odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
</Query>

var configurationValue = ConfigurationValues
	.Where(cv => cv.Key == "AuthenticationLockoutAttempts" && cv.ApplicationName == "CoreSecurityService")
	.Take(1)
	.ToList()
	.First();
	
	
configurationValue.Value = "3";

UpdateObject(configurationValue);
SaveChanges();

ConfigurationValues.OrderByDescending(cv => cv.ConfigurationValueId).Dump();