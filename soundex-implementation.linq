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
         Debug.WriteLine($"SOUNDEX({str})");
         if (string.IsNullOrEmpty(str))
            return null;

         // remove things that aren't pronouncable
         var pronouncableChars = Regex.Replace(str, @"[^a-zA-Z]", " ", RegexOptions.Singleline)
            .Trim()
            .ToUpper()
            .Replace("CZ","C"); // oracle seems to replace CZ with C

         if (pronouncableChars.Length == 0) return null;
        
         var firstChar = pronouncableChars[0];

         // create initial soundex code
         var soundexEncoded = firstChar + string.Join(string.Empty, pronouncableChars.Substring(1).Select(SoundexEncode));

         // remove duplicated consecutive numbers, construct a new string checking the last char against current
         var noDuplicatedConsecutiveSounds = soundexEncoded.ToCharArray().Aggregate("", (acc, currentChar) =>
         char.IsDigit(currentChar) && acc[acc.Length - 1] == currentChar && acc[acc.Length - 1] != '.'
               ? acc 
               : acc + currentChar
         );

         return Regex.Replace(noDuplicatedConsecutiveSounds, @"[\.0]", string.Empty).PadRight(4, '0').Substring(0, 4);
      }

    /// encode a char as an english phoneme
    public static string SoundexEncode(char c)
    {
        switch (char.ToLower(c))
        {
            case 'a': case 'e': case 'i': case 'o': case 'u': case 'y':
                return "0";
            case 'b':
            case 'f':
            case 'p':
            case 'v':
                return "1";
            case 'c':
            case 'g':
            case 'j':
            case 'k':
            case 'q':
            case 's':
            case 'x':
            case 'z':
                return "2";
            case 'd':
            case 't':
                return "3";
            case 'l':
                return "4";
            case 'm':
            case 'n':
                return "5";
            case 'r':
                return "6";
            default:
                return ".";
        }
    }

   }