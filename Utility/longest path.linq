<Query Kind="Program" />

// Define other methods and classes here
static string _MaxPath = "";
static void Main(string[] args)
{
	RecursePath(@"c:\");
	Console.WriteLine("Maximum path length is " + _MaxPath.Length);
	Console.WriteLine(_MaxPath);
	//Console.ReadLine();
}

static void RecursePath(string p)
{
	foreach (string d in Directory.GetDirectories(p))
	{
		if (IsValidPath(d))
		{
			foreach (string f in Directory.GetFiles(d))
			{
				if (f.Length > _MaxPath.Length)
				{
					_MaxPath = f;
				}
			}
			RecursePath(d);
		}
	}
}
static bool IsValidPath(string p)
{
	if ((File.GetAttributes(p) & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
	{
		Console.WriteLine("'" + p + "' is a reparse point. Skipped");
		return false;
	}
	if (!IsReadable(p))
	{
		Console.WriteLine("'" + p + "' *ACCESS DENIED*. Skipped");
		return false;
	}
	return true;
}
static bool IsReadable(string p)
{
	try
	{
		string[] s = Directory.GetDirectories(p);
	}
	catch (UnauthorizedAccessException ex)
	{
		return false;
	}
	return true;
}