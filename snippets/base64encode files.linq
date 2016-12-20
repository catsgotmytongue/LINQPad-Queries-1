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
	var filenames = new string[] {
					@"C:\Users\Public\Pictures\Sample Pictures\Desert.jpg",
					@"C:\Users\Public\Pictures\Sample Pictures\Jellyfish.jpg",
					@"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg"
	}.Select(s=>Convert.ToBase64String(System.IO.File.ReadAllBytes(s)));
	
	
	filenames.Dump();
	
}

// Define other methods and classes here