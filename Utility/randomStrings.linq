<Query Kind="Statements" />

var random = new Random();

new string( Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 25).Select(s => s[random.Next(s.Length)]).ToArray() ).Dump();