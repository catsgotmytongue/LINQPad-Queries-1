<Query Kind="Program">
  <Connection>
    <ID>25a7a408-c18e-4186-afce-3067194e405a</ID>
    <Persist>true</Persist>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://cenvapprd38.oya.ad/CoreDataService/odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
  <NuGetReference>State.OR.Oya.Core.Utility</NuGetReference>
  <Namespace>State.OR.Oya.Core.Utility.Caching</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Diagnostics</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.IO</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Logging</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Reflection</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.ServiceClient</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.String</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Threading</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Xml</Namespace>
</Query>

void Main()
{
	//JjisDataLinks.ToList().Dump();
	JjisNetDataLinks.ToList().OrderBy(dl=>dl.DataLinkId).Dump();
	//IdentificationNumbers.Select(idn => new { IdentificationType = "", IdentificationNumber  = idn.IdNumber, County = idn.IssuingCountyCode}).ToList().Dump();
}

// Define other methods and classes here
