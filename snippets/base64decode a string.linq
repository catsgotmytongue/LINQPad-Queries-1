<Query Kind="Statements">
  <NuGetReference>Microsoft.Owin</NuGetReference>
  <NuGetReference>Microsoft.Owin.Security</NuGetReference>
  <Namespace>Microsoft.Owin</Namespace>
  <Namespace>Microsoft.Owin.Security.DataHandler.Encoder</Namespace>
</Query>

var a = System.Text.Encoding.Default.GetString(Microsoft.Owin.Security.DataHandler.Encoder.TextEncodings.Base64Url.Decode("ZDBlNGI1NGYyODg3NGM3OTRiZmVjMjAzNDZmNzMxMDk"));
a.Dump();
a.Length.Dump();