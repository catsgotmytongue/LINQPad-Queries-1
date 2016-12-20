<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xml.Serialization.dll</Reference>
  <NuGetReference>State.OR.Oya.Core.Utility</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Caching</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Diagnostics</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.IO</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Logging</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Reflection</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.ServiceClient</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.String</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Threading</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Xml</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var localDatalinksUrl = "http://localhost:2403/datalinks";
	
	var datalinksRepository = new DataLinksRepository(localDatalinksUrl);
	
	var tempDatalinks = GetDataLinksFromFile(@"c:\temp\datalinks.xml");
	foreach (var dl in tempDatalinks)
	{
		datalinksRepository.AddDataLink(dl);
	}

//	var alldl = datalinksRepository.GetAllDatalinks().Result;
//	foreach (var dli in alldl)
//	{
//		//dli.Dump();
//		datalinksRepository.RemoveDatalink(dli);
//	}
}

public List<DataLinkIdentifier> GetDataLinksFromFile(string pathToXmlFile)
{
	using (StreamReader reader = new StreamReader(pathToXmlFile))
	{
		return ((ArrayOfJjisNetDataLink)(new XmlSerializer(typeof(ArrayOfJjisNetDataLink),String.Empty)).Deserialize(reader)).JjisNetDataLink.ToList();
	}
}

class DataLinksRepository
{
	private List<DataLinkIdentifier> datalinks;
	private string _repositoryBase;
	
	public DataLinksRepository(string url)
	{
		_repositoryBase = url;
	}
	
	public List<DataLinkIdentifier> Datalinks => datalinks;

	public async Task< List<DataLinkIdentifier> > GetAllDatalinks()
	{
		using (var client = new HttpClient())
		{
			var allDls = await client.GetStringAsync(_repositoryBase).ConfigureAwait(false);
			var jArray = JArray.Parse(allDls);
			return jArray.Select(p => (DataLinkIdentifier) p.ToObject(typeof (DataLinkIdentifier))).ToList();
		}
	}

	public async void AddDataLink(DataLinkIdentifier dli)
	{
		using (var client = new HttpClient())
		{
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var jsonString = JObject.FromObject(dli).ToString();
			//jsonString.Dump();
			var result = await client.PostAsync(_repositoryBase, new StringContent(jsonString, Encoding.UTF8, "application/json")).ConfigureAwait(false);
			
			if(result.StatusCode != System.Net.HttpStatusCode.OK)
				result.Dump();
		}
	}

	public async void RemoveDatalink(DataLinkIdentifier dli)
	{
		using (var client = new HttpClient())
		{
			var result = await client.DeleteAsync($"{_repositoryBase}/{dli.Id}").ConfigureAwait(false);

			if (result.StatusCode != System.Net.HttpStatusCode.OK)
				result.Dump();
		}
	}

}



/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class ArrayOfJjisNetDataLink
{

	private DataLinkIdentifier[] jjisNetDataLinkField;

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("JjisNetDataLink")]
	public DataLinkIdentifier[] JjisNetDataLink
	{
		get
		{
			return this.jjisNetDataLinkField;
		}
		set
		{
			this.jjisNetDataLinkField = value;
		}
	}
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataLinkIdentifier
{
	private string idField;
	private ulong dataLinkIdField;

	private string nameField;

	private string descriptionField;

	private string identifierField;

	private string entityClassNameField;

	private string expressionField;

	private string legacyDataLinkIdField;

	private string returnTypeCodeField;

	private string returnDomainField;

	private object lastChangedDateField;

	private object jjisTemplateGroupDataLinksField;

	public string Id
	{
		get { return this.idField;}
		set { this.idField = value;}
	}
	
	/// <remarks/>
	public ulong DataLinkId
	{
		get
		{
			return this.dataLinkIdField;
		}
		set
		{
			this.dataLinkIdField = value;
		}
	}

	/// <remarks/>
	public string Name
	{
		get
		{
			return this.nameField;
		}
		set
		{
			this.nameField = value;
		}
	}

	/// <remarks/>
	public string Description
	{
		get
		{
			return this.descriptionField;
		}
		set
		{
			this.descriptionField = value;
		}
	}

	/// <remarks/>
	public string Identifier
	{
		get
		{
			return this.identifierField;
		}
		set
		{
			this.identifierField = value;
		}
	}

	public string Domain
	{
		get
		{
			return this.entityClassNameField;
		}
		set
		{
			this.entityClassNameField = value;
		}
	}

	/// <remarks/>
	public string Expression
	{
		get
		{
			return this.expressionField;
		}
		set
		{
			this.expressionField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
	public string LegacyDataLinkId
	{
		get
		{
			return this.legacyDataLinkIdField;
		}
		set
		{
			this.legacyDataLinkIdField = value;
		}
	}

	/// <remarks/>
	public string ReturnTypeCode
	{
		get
		{
			return this.returnTypeCodeField;
		}
		set
		{
			this.returnTypeCodeField = value;
		}
	}

	/// <remarks/>
	public string ReturnDomain
	{
		get
		{
			return this.returnDomainField;
		}
		set
		{
			this.returnDomainField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
	public object LastChangedDate
	{
		get
		{
			return this.lastChangedDateField;
		}
		set
		{
			this.lastChangedDateField = value;
		}
	}

	/// <remarks/>
	public object JjisTemplateGroupDataLinks
	{
		get
		{
			return this.jjisTemplateGroupDataLinksField;
		}
		set
		{
			this.jjisTemplateGroupDataLinksField = value;
		}
	}
}