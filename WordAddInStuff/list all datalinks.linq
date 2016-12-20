<Query Kind="Program">
  <Connection>
    <ID>fa581a87-8c8a-4533-8811-093473d27ab8</ID>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://services-dev.jjis.oya.ad/CoreDataService/odata</Uri>
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
	var dlinks = JjisNetDataLinks.ToList();
	
	XmlSerializationUtility ser = new XmlSerializationUtility();
	
	var s = ser.Serialize(dlinks);
	System.IO.File.WriteAllText(@"c:\temp\datalinks.xml",s);
	
	//dlinks.Where(d=>d.ReturnDomain != null).OrderBy(dl=>dl.DataLinkId).Dump();
	//IdentificationNumbers.Select(idn => new { IdentificationType = "", IdentificationNumber  = idn.IdNumber, County = idn.IssuingCountyCode}).ToList().Dump();
}

// Define other methods and classes here