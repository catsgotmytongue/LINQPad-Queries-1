<Query Kind="Statements" />

var names = new string[] { "one", "two", "three", "four", "Five" };
var nums = new [] { 1, 2, 3, 4,5,6,7,8,9,10 };

var zipped = names.Zip(nums, (s , i)=>s +" "+ i);
zipped.Dump();