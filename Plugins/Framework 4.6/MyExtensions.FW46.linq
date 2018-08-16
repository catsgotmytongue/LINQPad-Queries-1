<Query Kind="Program" />

void Main()
{
	try
	{
		int t = 15;
		var binStrings = Enumerable.Range(0, 33).Select(num => new { binary = num.ToBinaryString(8), intNum = num });
		binStrings.ForEach(bs => $"{bs.binary} - {bs.binary.BinToHex()} - {bs.intNum}".Dump()); 
	}
	catch (Exception ex)
	{
		ex.Dump();
	}
}

public  static partial class MyExtensions
{
	public static string BinToHex(this string str)
	{
		var sb = new StringBuilder(str);
		while (sb.Length % 4 != 0) sb.Insert(0, "0");
		return sb.ToString().Partition(4).Aggregate("", (acc, nibble) => acc + NibbleToHex(nibble));
	}

	static string NibbleToHex(string nibble)
	{
		int nibbleVal = nibble.BinToInt();
		//nibbleVal.Dump("nibble");
		return nibble.Match()
				.With(s => s == "0000", "0")
				.With(s => s == "0001", "1")
				.With(s => s == "0010", "2")
				.With(s => s == "0011", "3")
				.With(s => s == "0100", "4")
				.With(s => s == "0101", "5")
				.With(s => s == "0110", "6")
				.With(s => s == "0111", "7")
				.With(s => s == "1000", "8")
				.With(s => s == "1001", "9")
				.With(s => s == "1010", "A")
				.With(s => s == "1011", "B")
				.With(s => s == "1100", "C")
				.With(s => s == "1101", "D")
				.With(s => s == "1110", "E")
				.Else("F");
	}

	public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> list, int size)
	{
		int i = 0;
		while( i * size < list.Count() ) yield return list.Skip(i++*size).Take(size);	
	}
	
	public static IEnumerable<string> Partition(this string str, int size) => str.Partition<char>(size).Select(AsString);
	
	public static string AsString(this IEnumerable<char> charList) => new string(charList.ToArray());
	
	public static int BinToInt(this string str)
	{
		return "F".ToCharArray().Aggregate(0, (acc, c) => { return acc + (2 ^ ("0123456789ABCDEF".IndexOf(c))); });
	}
	
	public static string ConvertExpandExpressionToJson(this string str)
		{
			var strReplaceWithJsonTokens = str.Replace("($expand=", ": {").Replace(")", "}");
			var pattern = @"([A-Za-z_]+)[(,)(\s*\})]";
			var replacement = @"""$1"":"""",";
			var newStr = Regex.Replace(strReplaceWithJsonTokens, pattern, replacement);
			return $"{{\n{newStr}\n}}";
		}

		public static bool ValidParens(this string str)
		{
			Stack<char> parens = new Stack<char>();
			int i;
			for (i = 0; i < str.Length; i++)
			{
				//str[i].Dump();
				if (str[i] == '(')
				{
					parens.Push('(');
				}
				if (str[i] == ')')
				{
					if (parens.IsEmpty()) return false;

					parens.Pop();
				}
			}

			return parens.IsEmpty();
		}

		public static int FindMatchingParenIndex(this string str, int startingParenIndex)
		{
			Stack<char> parens = new Stack<char>();
			int i;
			for (i = startingParenIndex; i <= str.Length; i++)
			{
				//str[i].Dump();
				if (str[i] == '(')
				{
					parens.Push('(');
					//"Push".Dump();
				}
				if (str[i] == ')')
				{
					parens.Pop();
					//"Pop".Dump();
				}

				if (parens.IsEmpty()) break;

			}

			return i + 1;
		}

		public static string TrimLengthTo(this string str, int length)
		{
			return str.Length > length ? str.Substring(0, length) : str;
		}

		//public static string ToBinaryString(this long n, long size = 32) => ToBinaryString((long)n, size);
		//public static string ToBinaryString(this int n, long size = 32) => ToBinaryString((dynamic)n, size);

	public static string ToBinaryString(this int n, int size = 32)
	{
		var f = Enumerable.Range(1,size);
		return f.Aggregate("", (acc, srcElement)=> (n & (1 << srcElement-1) ) != 0? "1"+acc: "0"+acc);
//			char[] b = new char[size];
//			long pos = size - 1;
//			int i = 0;
//
//			while (i < size)
//			{
//				if ((n & (1 << i)) != 0)
//				{
//					b[pos] = '1';
//				}
//				else
//				{
//					b[pos] = '0';
//				}
//				pos--;
//				i++;
//			}
//			return new string(b);
		}

		public static string ToHexString(this string binString)
		{
			var sb = new StringBuilder(binString);
			while (sb.Length % 4 != 0)
			{
				sb.Insert(0, "0", 1);
			}
			var bStr = sb.ToString();

			var hexDigits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
			var binMatch = Enumerable.Range(0, 16).Select(e => e.ToBinaryString(4)).ToList();
			var hexToBinConvert = hexDigits.Zip(binMatch, (c, s) => new KeyValuePair<string, char>(s, c)).ToDictionary(kv => kv.Key, kv2 => kv2.Value);

			var i = bStr.Length / 4;
			var sb2 = new StringBuilder();
			while (i != 0)
			{
				var s = bStr.Skip((i - 1) * 4).Take(4).Aggregate("", (s2, c) => s2 + c.ToString());
				sb2.Insert(0, hexToBinConvert[s].ToString(), 1);

				i--;
			}

			return sb2.ToString();
		}

		public static bool IsEven(this int n)
		{
			return (n & 1) == 0;
		}

		/// <summary>
		/// Checks to see if the collection is empty
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="source"></param>
		/// <returns>True if <paramref name="source" /></returns>
		public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return source.Count() == 0;
		}

		/// <summary>
		/// Checks to see if the collection does not have any objects matching the condition 
		/// in the <paramref name="predicate"/>
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="source"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return !source.Any(predicate);
		}

		public static DateTime GetTimeOfBuild(this string versionString)
		{
			var regexPattern = @"(?<major> [0-9]*)\.(?<minor> [0-9]*)\.(?<buildNo> [0-9]*)\.(?<revision> [0-9]*)";
			var regEx = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace);
			var isMatch = regEx.IsMatch(versionString);
			if (!isMatch)
			{
				throw new ArgumentException("Version string is invalid", "versionString");
			}

			var match = regEx.Match(versionString);

			var buildNo = System.Convert.ToDouble(match.Groups["buildNo"].Value);
			var revision = System.Convert.ToDouble(match.Groups["revision"].Value);

			var buildBeginEpoch = new DateTime(2000, 1, 1);
			var nd = buildBeginEpoch.AddDays(buildNo).AddSeconds(revision * 2);

			return nd;
		}

		// Write custom extension methods here. They will be available to all queries.
		public static string RemoveSpaces(this string str)
		{
			return str.RemoveAllOccurencesOf(" ");
		}

		public static string RemoveInvalidCSharpCharacters(this string str, params string[] additionalInvalidChars)
		{
			var invalidChars = new string[] { " ", "/", "-", "&", ",", ".", "(", ")" };
			invalidChars = invalidChars.Union(additionalInvalidChars).ToArray();
			var newString = str;

			newString = newString.RemoveAllOccurencesOf(invalidChars);


			return newString;
		}

		public static string RemoveOccurencesOf(this string str, string stringToRemove)
		{
			return str.Replace(stringToRemove, string.Empty);
		}

		public static string RemoveAllOccurencesOf(this string str, params string[] stringsToRemove)
		{
			var sb = new StringBuilder(str);

			foreach (string strToRemove in stringsToRemove)
			{
				sb.Replace(strToRemove, String.Empty);
			}
			return sb.ToString();
		}

		public static string DePascalized(this string str)
		{
			if (String.IsNullOrEmpty(str))
				return "";

			return String.Join("", str.Select(c => Char.IsUpper(c) ? " " + c : c.ToString()));
		}

		/// <summary>
		/// Remove all instances of whitespace in a string and return the resulting string
		/// Removes all spaces,tabs, newlines, and other whitespace chars from a string
		/// sometimes useful for comparing strings that don't need to be human 
		/// readable to have the same meaning.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string RemoveAllWhitespace(this string str)
		{
			return Regex.Replace(str, @"\s*", System.String.Empty);
		}

		public static double HoursBetween(this DateTime time1, DateTime time2)
		{
			return SecondsBetween(time1, time2) / 3600;
		}

		public static double SecondsBetween(this DateTime time1, DateTime time2)
		{
			return (Max(time1, time2) - Min(time1, time2)).TotalSeconds;
		}

		public static DateTime Max(this DateTime date, DateTime dateToCompare)
		{
			return date > dateToCompare ? date : dateToCompare;
		}

		public static DateTime Min(this DateTime date, DateTime dateToCompare)
		{
			return date < dateToCompare ? date : dateToCompare;
		}


	}

public static class MathUtil
{
	public static string FormatHours(this double hours)
	{
		//(hours // 24) displayString , ' days, ' , (hours \\ 24) displayString, ' hours'
		return $"{hours / 24:#.##} days, {hours % 24:#.##} hours";
	}

	public static string FormatDaysHoursAndMinutes(this double hours)
	{

		return $"{hours / 24:#.##} days, {hours % 24:#.##} hours, { (hours % 24) / 60 } minutes";
	}

	public static string FormatHours(this long hours)
	{
		//(hours // 24) displayString , ' days, ' , (hours \\ 24) displayString, ' hours'
		return $"{hours / 24:#.##} days, {hours % 24:#.##} hours";
	}

	private static string HoursAndMinutesDynamic(dynamic minutes)
	{
		return $"{minutes / 60:#.##} hours, { (minutes % 60)} minutes";
	}

	public static string HoursAndMinutes(this double minutes) => HoursAndMinutesDynamic(minutes);
	public static string HoursAndMinutes(this long minutes) => HoursAndMinutesDynamic(minutes);

	public static double DivideBy(this long d, long divisor) => Math.Round((double)d / divisor, 8);

}

public static class Disposable
{
	/// <summary>
	/// Sample usage: Disposable.Using(() => System.IO.File.Open(), file=>file.Rea
	/// </summary>
	/// <typeparam name="TDisposable"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <param name="factory"></param>
	/// <param name="fn"></param>
	/// <returns></returns>
	public static TResult Using<TDisposable, TResult>
	(
		Func<TDisposable> factory,
		Func<TDisposable, TResult> fn)
		where TDisposable : IDisposable
	{
		using (var disposable = factory())
		{
			return fn(disposable);
		}
	}
}

public static class FunctionalExtensions
{
	/// <summary>
	/// Perform an <paramref name="action"/> on <paramref name="@this"/> and return <paramref name="@this"/> instance
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="this"></param>
	/// <param name="action"></param>
	/// <returns></returns>
	public static T Tee<T>(this T @this, Action<T> action)
	{
		action(@this);
		return @this;
	}

	/// <summary>
	/// Map from <paramref name="@this"/> to the resulting object returned by <paramref name="fn"/>
	/// </summary>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <param name="this"></param>
	/// <param name="fn"></param>
	/// <returns></returns>
	public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> fn) => fn(@this);

	/// <summary>
	/// When the predicate is true return the result of <paramref name="fn"/>(@this) otherwise return <paramref name="@this"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="this"></param>
	/// <param name="predicate"></param>
	/// <param name="fn"></param>
	/// <returns></returns>
	public static T When<T>(this T @this, Func<bool> predicate, Func<T, T> fn) => predicate() ? fn(@this) : @this;

	/// <summary>
	/// Perform an <paramref name="action"/> on each element in <paramref name="source"/>. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="source"></param>
	/// <param name="action"></param>
	/// <returns></returns>
	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (var x in source)
			action(x);
	}

	/// <summary>
	/// Returns everything in <paramref name="source"/> without <paramref name="obj"/>
	/// Wraps the Ienumerable version of Except with a single exception
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="source"></param>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static IEnumerable<T> ExceptOne<T>(this IEnumerable<T> source, T obj)
	{
		return source.Tee(l => l.ToList().Remove(obj)).ToList();
	}

	public static IEnumerable<T> ExceptOne<T>(this IEnumerable<T> source, T obj, IEqualityComparer<T> comparer)
	{
		return source.Tee(l => l.ToList().Remove(obj)).ToList();
	}
}

/// <summary>
/// Comparer that puts alphanumeric strings in correct order when thinking of how humans read
/// e.g. file1,file2, file20, file10, file101 are ordered by alpha and numeric ordering:
///      file1,file2,file10,file20,file101
/// </summary>
public class NaturalOrderComparer : IComparer<string>
{
	// are they both numeric or both alpha numeric ?
	private static bool SimilarChars(char ch, char otherCh) => IsDigit(ch) == IsDigit(otherCh);
	private static bool IsDigit(char ch) => char.IsDigit(ch);

	/// <summary>
	/// Compare two strings in an alphanumeric ordering scheme
	/// </summary>
	/// <param name="str1"></param>
	/// <param name="str2"></param>
	/// <returns></returns>
	public int Compare(string str1, string str2)
	{
		if (str1 == null || str2 == null)
			return 0;

		var str1Index = 0;
		var str2Index = 0;
		while (str1Index < str1.Length || str2Index < str2.Length)
		{
			if (str1Index >= str1.Length)
				return -1;

			if (str2Index >= str2.Length)
				return 1;

			var str1Tok = GetNextToken(str1, ref str1Index);
			var str2Tok = GetNextToken(str2, ref str2Index);

			var result = 0;
			result = IsDigit(str1Tok[0]) && IsDigit(str2Tok[0])
				? CompareNumeric(str1Tok, str2Tok)
				: String.Compare(str1Tok, str2Tok, StringComparison.Ordinal);

			if (result != 0)
				return result;
		}

		return 0;
	}

	/// <summary>
	/// advance the index, while retrieving the next token
	/// </summary>
	/// <param name="str"></param>
	/// <param name="startIndex"></param>
	/// <returns></returns>
	private static String GetNextToken(string str, ref int startIndex)
	{
		var ch = str[startIndex];
		var tokenString = new StringBuilder();
		while (startIndex < str.Length && (tokenString.Length == 0 || SimilarChars(ch, tokenString[0])))
		{
			tokenString.Append(ch);
			startIndex++;

			if (startIndex < str.Length)
				ch = str[startIndex];
		}
		return tokenString.ToString();
	}

	private static int CompareNumeric(String str1Tok, String str2Tok)
	{
		var result = 0;

		var numTok1 = Convert.ToInt32(str1Tok);
		var numTok2 = Convert.ToInt32(str2Tok);

		if (numTok1 < numTok2)
			result = -1;

		if (numTok1 > numTok2)
			result = 1;
		return result;
	}
}

public static class PatternMatch
{
	public static Pattern<T> Match<T>(this T value) => new Pattern<T>(value);
	public class Pattern<T>
	{
		private readonly T _value;
		public PatternCase<T, TResult> With<TResult>(Predicate<T> condition, TResult result)
			=> new PatternCase<T, TResult>(_value).With(condition, result);
		internal Pattern(T value) => _value = value;
	}
	public class PatternCase<T, TResult>
	{
		private readonly IList<PatternMatchCase> _cases = new List<PatternMatchCase>();
		private readonly T _value;
		private Func<T, TResult> _elseCase;
		public PatternCase(T value) => _value = value;
		public PatternCase<T, TResult> With(Predicate<T> condition, Func<T, TResult> result)
		{
			_cases.Add(new PatternMatchCase { Condition = condition, Result = result });
			return this;
		}
		public PatternCase<T, TResult> With(Predicate<T> condition, TResult result)
			=> With(condition, x => result);
		public PatternCase<T, TResult> Else(Func<T, TResult> result)
		{
			if (_elseCase != null) throw new InvalidOperationException("Only one else is allowed");
			_elseCase = result;
			return this;
		}
		
		public TResult Else(TResult result)
		{
			Else(x => result);
			if (_elseCase != null)
			{
				With(x => true, _elseCase);
				_elseCase = null;
			}

			foreach (var @case in _cases)
				if (@case.Condition(_value)) return @case.Result(_value);
			throw new IncompletePatternException();
		}
		private struct PatternMatchCase
		{
			public Predicate<T> Condition;
			public Func<T, TResult> Result;
		}
	}
	public class IncompletePatternException : Exception { }
}