<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.dll</Reference>
  <Namespace>System.CodeDom</Namespace>
  <Namespace>System.CodeDom.Compiler</Namespace>
</Query>

void Main()
{


	var f = System.IO.File.ReadAllText(@"C:\temp\octoscript.ps1");
	var str = ToLiteral(f);
//	Console.WriteLine(f);
}

// Define other methods and classes here
private static string ToLiteral(string input)
{
    using (var writer = new StringWriter())
    {
        using (var provider = CodeDomProvider.CreateProvider("CSharp"))
        {
            provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
            return writer.ToString();
        }
    }
}