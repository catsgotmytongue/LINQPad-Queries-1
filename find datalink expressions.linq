<Query Kind="Program">
  <Connection>
    <ID>a3839723-1f14-4b0a-8d14-93952fb67c4f</ID>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://services-dev.jjis.oya.ad/CoreDataService/Odata</Uri>
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
	var a = JjisNetDataLinks.Expand("LegacyDataLink").ToList();
	//a.Dump();
	var datalinks = a.Where(
		//j => j.DataLinkId == 966907609457
		j=>j?.LegacyDataLink != null && j.LegacyDataLink.DataLinkMessages.Contains("hasTwoOrMoreReferralsWithRunawayPastYearDL")
		/*j.Identifier.Contains("PoliceReportNumber")*/
//		   j.Identifier == "Youth_MostRecentReferralPoliceReportNumber" 
// 		|| j.Identifier =="Youth_MostRecentOpenReferralPoliceReportReportNumber"
		);
	datalinks.Dump();

	var datalink = datalinks.Select(d=>new {Expression=d.Expression, LegacyDatalinkMessages=d.LegacyDataLink.DataLinkMessages}).ToList();

	foreach (var dl in datalink)
	{
		dl.Expression.Dump();
		dl.LegacyDatalinkMessages.Dump();
		"".Dump();
	}
	
}

// Define other methods and classes here