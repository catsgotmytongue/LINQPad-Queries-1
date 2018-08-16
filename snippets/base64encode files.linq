<Query Kind="Program" />

void Main()
{
	var filenames = new string[] {
					@"C:\Users\Public\Pictures\Sample Pictures\Desert.jpg",
					@"C:\Users\Public\Pictures\Sample Pictures\Jellyfish.jpg",
					@"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg"
	}.Select(s=>Convert.ToBase64String(System.IO.File.ReadAllBytes(s)));
	
	
	filenames.Dump();
	
}

// Define other methods and classes here