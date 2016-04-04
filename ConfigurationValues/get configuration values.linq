<Query Kind="Expression">
  <Connection>
    <ID>25a7a408-c18e-4186-afce-3067194e405a</ID>
    <Persist>true</Persist>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://cenvapprd38.oya.ad/CoreDataService/odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
</Query>

ConfigurationValues.Where(cv=>cv.ApplicationName == "JJIS.API")