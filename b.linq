<Query Kind="Program">
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
	var objects = new[]
	{
		new { PrimaryFlag = (bool?) true},
		new { PrimaryFlag = (bool?) false},
		new { PrimaryFlag = (bool?) null},
	};

	objects.OrderBy(d => d.PrimaryFlag).Dump();
	objects.OrderByDescending(d => d.PrimaryFlag).Dump();
}

// Define other methods and classes here
