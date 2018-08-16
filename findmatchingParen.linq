<Query Kind="Program" />

void Main()
{
	var str = "A(B(C(D)fkgjg))";
	var t = str.FindMatchingParenIndex(3).Dump();
	str.Substring(3, t-3).Dump();
	"A(B(C(D)))".FindMatchingParenIndex(5).Dump();
}