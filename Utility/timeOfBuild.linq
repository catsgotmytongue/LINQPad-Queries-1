<Query Kind="Program" />

string GenerateBuildNumber(int major, int minor, DateTime timeOfBuild)
{
	var buildBeginEpoch = new DateTime(2000, 1, 1);
	
	//var regexPattern = @"(?<major> [0-9]*)\.(?<minor> [0-9]*)\.(?<buildNo> [0-9]*)\.(?<revision> [0-9]*)";
	//var now = DateTime.Now;
	// build number is the number of days since beginEpoch
	var buildNo = (int)(timeOfBuild - buildBeginEpoch).TotalDays;	
	
	var revision = CalculateFractionalPartOfDay(timeOfBuild);
	
	return $"{major}.{minor}.{buildNo}.{revision}";

}

private int CalculateFractionalPartOfDay(DateTime date)
{
	//break down a day into fractional seconds
	float factor = (float)(UInt16.MaxValue - 1) / (24 * 60 * 60);

	return (int)(date.TimeOfDay.TotalSeconds * factor);
}

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
	var dateOfBuild = new DateTime(2015, 12, 8, 2, 3, 36, DateTimeKind.Local);
	var major = 1;
	var minor = 7;
	var buildno = "5820";
	var rev = "25308";
	var desiredBuildNo = $"{major}.{minor}.{buildno}.{rev}";
	
	desiredBuildNo.Dump();
	
	GetTimeOfBuild(desiredBuildNo).Dump();
	
	GenerateBuildNumber(1,7, dateOfBuild).Dump();
}

// Define other methods and classes here