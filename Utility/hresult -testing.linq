<Query Kind="Program" />

void Main()
{
    //              2148270088
	Int32 hresult = -2146697208; // 800C0008
	($"HRESULT: {hresult:X}").Dump();

	var binStr = hresult.ToBinaryString();
	($"Binary: {binStr}").Dump();
	
	var remainingBitCount = 32;
	
	var sev = hresult & 0xB0000000 ;
	remainingBitCount -=2;
	($"Severity: {sev>>remainingBitCount:X}").Dump();

    // get bit by itself by ANDing with 0010 
	var c = hresult &   0x20000000;
	remainingBitCount-=1;
	($"c: {c >> 32 - 3:X}").Dump();

	var n = hresult & 0x10000000;
	remainingBitCount -= 1;
	($"n: {n >> 32 - 4:X}").Dump();


	var x = hresult & 0x08000000;
	remainingBitCount -= 1;
	($"x: {x >> 32 - 5:X}").Dump();

	var facility = hresult & 0x0EFFC000;
	remainingBitCount-=11;
	($"Facility: {facility >> remainingBitCount:X}").Dump();

	var errorCode = hresult & 0x0000FFFF;
	remainingBitCount -= 16;
	($"Error Code: {errorCode >> remainingBitCount:X}").Dump();
}

// Define other methods and classes here