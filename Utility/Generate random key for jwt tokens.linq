<Query Kind="Program">
  <NuGetReference>Microsoft.Owin.Security</NuGetReference>
  <Namespace>Microsoft.Owin.Security.DataHandler.Encoder</Namespace>
</Query>

static int[] ValidKeyLengths = new int[] {32, 48,64};
static int keyLength = 32;

void Main()
{
	var base64Length = keyLength;
	var randomKeys = GetRandomKeyArray(10, base64Length);
	String.Join(String.Format("\n\n", base64Length), randomKeys.Select(k => String.Format("{0} -- {1}", k, CheckValidKey(k)))).Dump();


	//DecodeKey("OTgxYTBhMzI5MGQzZjNkYTkxNWQ3ZjVmMjBhM2MyYTRkNGE4NmQzYjMxYzgyOA==").Length.Dump();
	//DecodeKey("U1hoeVFXcEViMkV5Um5GRmJFODNTV2h5VTNKVlNrVk1hRlZqYTJWUVJWQldjR0Zs").Length.Dump();

}


string GetRandomKey(int base64Length, int keyLength)
{
	//var keyLength = (int)Math.Ceiling((base64Length-3)*((double)3/4));
	if(!ValidKeyLengths.Contains(keyLength))
	{
		throw new InvalidOperationException(String.Format("Valid key Lengths are: {0}", String.Join(", ", ValidKeyLengths)));
	}
	var guids = Guid.NewGuid().ToString("n")+Guid.NewGuid().ToString("n")+Guid.NewGuid().ToString("n")+Guid.NewGuid().ToString("n");
	
	var randomizedString = guids.Substring((new Random()).Next(0,keyLength), keyLength);
	
	// randomly grab keyLength chars from the encoded string
	var bytes = Encoding.UTF8.GetBytes(randomizedString);
		
	return TextEncodings.Base64.Encode(bytes);
}


string[] GetRandomKeyArray(int length, int base64Length)
{
	string[] keys = new string[length];
	for(int i = 0; i < length; i++)
	{
		keys[i] = GetRandomKey(base64Length, keyLength);
	}

	return keys;
}

string DecodeKey(string key) {
	var bytes = TextEncodings.Base64.Decode(key);
	return Encoding.UTF8.GetString(bytes);	
}

bool CheckValidKey(string key) {
	var decodedKey = DecodeKey(key);
	var keyLength = decodedKey.Length;
	return ValidKeyLengths.Contains(keyLength);	
}



// Define other methods and classes here