<Query Kind="Expression">
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

//AddCustomDocumentProperty(Globals.ThisAddIn.Application.ActiveDocument, new WordDocumentProperty());
var builtInProps = (Microsoft.Office.Core.DocumentProperties)Application.ActiveDocument.BuiltInDocumentProperties;
var random = new Random();
Func<int, String> randString =
	(int length) =>
		new string(
			Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", length)
				.Select(s => s[random.Next(s.Length)])
				.ToArray());

for (int i = 0; i < 200; i++)
{
	AddCustomDocumentProperty(Application.ActiveDocument,
		new WordDocumentProperty() { Name = $"CustomProp{i}", Value = randString(4000) });
}
//var prop = GetCustomDocumentProperty(this.Application.ActiveDocument, "JJISCustomProp");

var propList = GetDocumentPropertyList(builtInProps);
var propList2 = GetDocumentPropertyList((Microsoft.Office.Core.DocumentProperties)Application.ActiveDocument.CustomDocumentProperties);