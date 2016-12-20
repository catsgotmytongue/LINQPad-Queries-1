<Query Kind="Statements">
  <Connection>
    <ID>fa581a87-8c8a-4533-8811-093473d27ab8</ID>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://services-dev.jjis.oya.ad/CoreDataService/odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
</Query>

// Set the codeType variable to the desired code and run script to generate code
var codeType = "ECAT";

var tab = "    ";
var indent3 = tab + tab + tab;
var indent4 = indent3 + tab;
var sb = new StringBuilder(indent3);

// Query for data
var codeTypeDescription = Codes.Where(c => c.Type == "CODT" && c.CodeValue == codeType).Take(1).FirstOrDefault();
var codes = Codes
	.Where(c => c.Type == codeType)
	.OrderBy(c => c.Description)
	.Select(c => new { c.Description, c.CodeValue });

// Build C# string
sb.AppendLine($"public static class {codeTypeDescription.Description.RemoveInvalidCSharpCharacters()}");
sb.AppendLine($"{indent3}{{");


foreach (var code in codes)
{
	sb.AppendLine($"{indent4}public const string {code.Description.RemoveInvalidCSharpCharacters()} = \"{code.CodeValue}\";");
}

sb.AppendLine($"{indent3}}}");

// Dump the C# string
sb.ToString().Dump();