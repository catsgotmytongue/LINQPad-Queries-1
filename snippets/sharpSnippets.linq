<Query Kind="Program">
  <NuGetReference>State.OR.Oya.Core.Utility</NuGetReference>
  <Namespace>State.OR.Oya.Core.Utility.Caching</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Diagnostics</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.IO</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Logging</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Reflection</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.ServiceClient</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.String</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Threading</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Xml</Namespace>
</Query>

void Main()
{
	
}

// snippets

public async Task<TResponse> PostAsBsonAsync<TResponse>(string url, object data, MediaTypeWithQualityHeaderValue acceptHeaderValue = null, MediaTypeFormatter[] mediaTypeFormatters = null)
{
	using (var stream = new MemoryStream())
	{
		using (var bsonWriter = new BsonWriter(stream))
		{
			var jsonSerializer = new JsonSerializer();
			jsonSerializer.Serialize(bsonWriter, data);

			var byteArrayContent = new ByteArrayContent(stream.ToArray());
			byteArrayContent.Headers.ContentType = BsonContentTypeHeaderValue;

			acceptHeaderValue = acceptHeaderValue ?? BsonAcceptHeaderValue;
			mediaTypeFormatters = mediaTypeFormatters != null && mediaTypeFormatters.Any()
				? mediaTypeFormatters
				: new MediaTypeFormatter[] { BsonMediaTypeFormatter };

			using (var client = CreateHttpClient(acceptHeaderValue))
			{
				var response = await client.PostAsync(url, byteArrayContent);
				return await response.Content.ReadAsAsync<TResponse>(mediaTypeFormatters);
			}
		}
	}
}




//private static WordprocessingDocument OpenWordprocessingOrFlatOpcDoc(MemoryStream memStream, bool isEditable)
//{
//    var isFlatOpc = IsFlatOpc(memStream);

//    if (memStream.CanSeek)
//        memStream.Seek(0, SeekOrigin.Begin);

//    var sr = new StreamReader(memStream, Encoding.UTF8);
//    var text = sr.ReadToEnd();
//    return isFlatOpc 
//        ? WordprocessingDocument.FromFlatOpcString(text, new MemoryStream(), isEditable) 
//        : WordprocessingDocument.Open(memStream, isEditable);
//}

private static bool IsFlatOpc(MemoryStream memStream)
{
	var originalPosition = memStream.Position;
	memStream.Seek(0, SeekOrigin.Begin);
	var buf = new byte[5];
	memStream.Read(buf, 0, 5);

	bool isFlatOpc = false;

	var xmlStrMarker = System.Text.Encoding.UTF8.GetString(buf);

	if (xmlStrMarker == "<?xml")
		isFlatOpc = true;

	memStream.Position = originalPosition;
	return isFlatOpc;
}

public static IOrderedEnumerable<T> ReverseOrder<T>(this IEnumerable<T> source) where T : class
{
	int order = 0;
	var objOrder = new Dictionary<object, int>();
	var sourceReversed = source.Reverse().Select(o =>
	{
		objOrder.Add(o, order++);
		return o;
	});

	return sourceReversed.OrderBy(o => objOrder[o]);
}

public static void CreatePluginKernel()
{
	// bind all the plugin classes
	var kernel = new StandardKernel();
	kernel.Bind(scanner => scanner.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
							   .SelectAllClasses()
							   .InheritedFrom<IPlugin>()
						   .BindToAllInterfaces());
}