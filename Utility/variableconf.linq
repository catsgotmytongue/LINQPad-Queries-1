<Query Kind="Program">
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
	var dict = new Dictionary<string, string>() {
		{"ENV","TST"},
		{"BASEDIR",@"c:\temp\$ENV$"},
		{"BASEDIR2",@"c:\temp\$EXE$\$ENV$"},
		{"EXE",@"$BASEDIR$\bin"},
		{"blah",@"$EXE$\ENV\Blah"},
	};
	VariableConfiguration vc = new VariableConfiguration(dict);

	vc.GetValue(dict["blah"]).Dump();
	vc.GetValue(dict["BASEDIR2"]).Dump();
}

public class VariableConfiguration
{
	private IDictionary<string, string> _evalDict;
	private static Regex _regex;
	
	static VariableConfiguration()
	{
		string pattern = @"\$(?<KeyVal>[A-Za-z_]*)\$";
		RegexOptions options = RegexOptions.Singleline | RegexOptions.IgnoreCase;
		_regex = new Regex(pattern, options);
	}

	public VariableConfiguration(IDictionary<string, string> evalDict)
	{
		_evalDict = evalDict;
	}

	public string GetValue(string val)
	{
		if (String.IsNullOrEmpty(val))
			return val;
		
		val = _regex.Replace(val, KeyReplace);
		
		if(_regex.IsMatch(val))
			val = GetValue(val);
		
		return val;
	}

	private string KeyReplace(Match m)
	{
		string key = m.Groups["KeyVal"].Value;

		if (_evalDict.ContainsKey(key))
			return _evalDict[key];
			
		return key;
	}


}