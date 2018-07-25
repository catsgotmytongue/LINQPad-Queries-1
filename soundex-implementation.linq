<Query Kind="Program" />

void Main()
{
	var dictionary = new List<string>() {
		"Johny",
		"Jones",
		"joans",
		"James",
		"Johnnnnyyy"
	};

	foreach (var word in dictionary)
	{
		SoundexUtil.Soundex(word).Dump($"Soundex of '{word}': ");
	}
}

public static class SoundexUtil
{
	public static string Soundex(string str)
	{
		if (string.IsNullOrEmpty(str)) 
			return null;

		// remove things that aren't pronouncable
		var pronouncableChars = Regex.Replace(str, @"[0123456789~!@#$%^&*()_\+=-]", string.Empty, RegexOptions.Singleline);
		
		// remove vowel-like sounds
		var noVowels = Regex.Replace(pronouncableChars, "[aehiouwy]", string.Empty, RegexOptions.Singleline);
		
		// create initial soundex code
		var soundexEncoded = char.ToUpper(pronouncableChars[0]) + String.Join( string.Empty, noVowels.Substring(1).Select(SoundexEncode) );
		soundexEncoded.Dump("soundex encoded: ");
		// remove duplicated consecutive numbers, construct a new string checking the last char against current
		var noDuplicatedConsecutiveSounds = soundexEncoded.ToCharArray().Aggregate("", (acc, c) =>  char.IsDigit(c) && acc[acc.Length-1] == c ? acc : acc+c );
				
		return noDuplicatedConsecutiveSounds.PadRight(4, '0');
	}

	/// encode a char as an english phoneme
	public static string SoundexEncode(char c)
	{
		switch (char.ToLower(c))
		{	
			case 'b': case 'f': case 'p': case 'v':
				return "1";
			case 'c': case 'g': case 'j': case 'k':  case 'q': case 's': case 'x': case 'z':
				return "2";
			case 'd': case 't':
				return "3";
			case 'l':
				return "4";
			case 'm': case 'n':
				return "5";
			case 'r': 
				return "6";
			default:
				return string.Empty;
		}
	}
}