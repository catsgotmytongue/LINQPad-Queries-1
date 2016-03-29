<Query Kind="Program" />

DateTime GetTimeOfBuild(string versionString)
{	
		var regexPattern = @"(?<major> [0-9]*)\.(?<minor> [0-9]*)\.(?<buildNo> [0-9]*)\.(?<revision> [0-9]*)";
		var regEx = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace);
		var isMatch = regEx.IsMatch(versionString);
		if(!isMatch)
		{
			throw new ArgumentException("Version string is invalid", "versionString");
		}
		
		var match = regEx.Match(versionString);
		
		var buildNo = System.Convert.ToDouble(match.Groups["buildNo"].Value);
		var revision = System.Convert.ToDouble(match.Groups["revision"].Value);
		
		var buildBeginEpoch = new DateTime(2000,1,1);
		var nd = buildBeginEpoch.AddDays(buildNo).AddSeconds(revision*2);
		
		return nd;
}

void Main()
{
	GetTimeOfBuild("1.7.5820.25308").Dump();
}

// Define other methods and classes here