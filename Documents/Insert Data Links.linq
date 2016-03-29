<Query Kind="Program">
  <Connection>
    <ID>836967e7-c6a0-40c1-ab6d-15d75f502367</ID>
    <Persist>true</Persist>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://cenvapprd38.oya.ad/CoreDataService/odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	long id = 0L;
	AddDataLink(id, "Youth FormattedName", "Youth_FormattedName", "YouthExtension.FormattedName(youth)", "Youth");
	
	id++;
	AddDataLink(id, "Entity FormattedName", "Entity_FormattedName", "EntityFormatter.FormattedName(jjisEntity)", "JjisEntity");

	id++;
	AddDataLink(id, "Youth Victims", "Youth_Victims", "YouthExtension.Victims(youth)", "JjisEntity");

	id++;
	AddDataLink(id, "Address (One Line)", "Address_OneLineAddressWithoutCountryCode", "AddressExtension.OneLineAddressWithoutCountryCode(address)", "Address");
}

void AddDataLink(long id, string name, string identifier, string expression, string entityClassName)
{
	var dataLink = new JjisNetDataLink
	{
		DataLinkId = id,
		Name = name,
		Identifier = identifier,
		Expression = expression,
		ReturnTypeCode = "S",
		EntityClassName = entityClassName,
		InstanceFlagAsString = "N"
	};

	AddObject("JjisNetDataLinks", dataLink);
	SaveChanges();
}

// Define other methods and classes here