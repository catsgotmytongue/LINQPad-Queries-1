<Query Kind="Program" />

void Main()
{
	foreach (var element in Enumerable.Range(0, 32))
	{
		element.ToBinaryString(4).ToHexString().Dump();
	}
}

// Define other methods and classes here