<Query Kind="Program" />

void Main()
{
	Func<int> closureFunc = Magic();
	for (int i = 1; i < 11; i++)
	{
		Console.WriteLine(closureFunc());
	}
}


Func<int> Magic()
{
	var localVar = 1;
	Func<int> Closure = () =>
	{
		localVar *= 2;
		return localVar;
	};

	return Closure;
}