<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var cmds = System.IO.File.ReadAllText(@"c:\temp\cmd.json");
	var commands = (Commands)Newtonsoft.Json.JsonConvert.DeserializeObject(cmds, typeof(Commands));

	HttpObjectRepository<CommandEntry> repo = new HttpObjectRepository<CommandEntry>(@"http://localhost:2403/info");
	
	foreach (var cmd in commands.Info)
	{
		repo.AddObject(cmd);
	}
}

// Define other methods and classes here
/// <summary>
/// Used in place of a database when testing - in place of the database
/// NOTE: this is not cached
/// </summary>
/// <typeparam name="T"></typeparam>
public class HttpObjectRepository<T> where T : IHttpCollectionItemObject
{
	private string _repositoryBase;
	private const string JsonContentHeader = "application/json";

	public HttpObjectRepository(string url)
	{
		_repositoryBase = url;
	}

	public virtual async Task<List<T>> GetAll()
	{
		using (var client = new HttpClient())
		{
			var objects = await client.GetStringAsync(_repositoryBase).ConfigureAwait(false);
			var jArray = JArray.Parse(objects);
			return jArray.Select(p => (T)p.ToObject(typeof(T))).ToList();
		}
	}

	/// <summary>
	/// Does a post to the http repo to add an object
	/// </summary>
	/// <param name="obj"></param>
	public virtual async void AddObject(T obj)
	{

		using (var client = new HttpClient())
		{
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonContentHeader));
			var jsonString = JObject.FromObject(obj).ToString();

			var jsonStringContent = new StringContent(jsonString, Encoding.UTF8, JsonContentHeader);
			var result = await client.PostAsync(_repositoryBase, jsonStringContent).ConfigureAwait(false);

			LogHttpErrorIfFailure(result);

		}
	}

	/// <summary>
	/// does a delete request to a http repository to remove the object from the collection
	/// </summary>
	/// <param name="obj"></param>
	public virtual async void RemoveObject(T obj)
	{
		using (var client = new HttpClient())
		{
			var result = await client.DeleteAsync($"{_repositoryBase}/{obj.id}").ConfigureAwait(false);

			LogHttpErrorIfFailure(result);
		}
	}

	private static void LogHttpErrorIfFailure(HttpResponseMessage result)
	{
		if (result.StatusCode != System.Net.HttpStatusCode.OK)
			System.Diagnostics.Trace.WriteLine($"error {result.StatusCode}: {result.ReasonPhrase}");
	}



}

public interface IHttpCollectionItemObject
{
	string id { get; set; }
}

public class Commands
{
	[JsonProperty("info")]
	public CommandEntry[] Info { get; set;}
}

public class CommandEntry : IHttpCollectionItemObject
{
	public string id { get; set; }

	public string command { get; set;}
	public string description { get; set; }
	public string Filename { get; set; }
	
}