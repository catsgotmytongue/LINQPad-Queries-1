<Query Kind="Program">
  <Reference>D:\git\StarTrekAttackWingApplication\StawModel\bin\Debug\StawModel.dll</Reference>
  <NuGetReference>Humanizer</NuGetReference>
  <NuGetReference>MongoDB.Driver</NuGetReference>
  <Namespace>Humanizer</Namespace>
  <Namespace>Humanizer.Bytes</Namespace>
  <Namespace>Humanizer.Configuration</Namespace>
  <Namespace>Humanizer.DateTimeHumanizeStrategy</Namespace>
  <Namespace>Humanizer.Inflections</Namespace>
  <Namespace>Humanizer.Localisation</Namespace>
  <Namespace>Humanizer.Localisation.CollectionFormatters</Namespace>
  <Namespace>Humanizer.Localisation.DateToOrdinalWords</Namespace>
  <Namespace>Humanizer.Localisation.Formatters</Namespace>
  <Namespace>Humanizer.Localisation.NumberToWords</Namespace>
  <Namespace>Humanizer.Localisation.Ordinalizers</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Bson.IO</Namespace>
  <Namespace>MongoDB.Bson.Serialization</Namespace>
  <Namespace>MongoDB.Bson.Serialization.Attributes</Namespace>
  <Namespace>MongoDB.Bson.Serialization.Conventions</Namespace>
  <Namespace>MongoDB.Bson.Serialization.IdGenerators</Namespace>
  <Namespace>MongoDB.Bson.Serialization.Options</Namespace>
  <Namespace>MongoDB.Bson.Serialization.Serializers</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Core.Authentication</Namespace>
  <Namespace>MongoDB.Driver.Core.Authentication.Sspi</Namespace>
  <Namespace>MongoDB.Driver.Core.Bindings</Namespace>
  <Namespace>MongoDB.Driver.Core.Clusters</Namespace>
  <Namespace>MongoDB.Driver.Core.Clusters.ServerSelectors</Namespace>
  <Namespace>MongoDB.Driver.Core.Configuration</Namespace>
  <Namespace>MongoDB.Driver.Core.ConnectionPools</Namespace>
  <Namespace>MongoDB.Driver.Core.Connections</Namespace>
  <Namespace>MongoDB.Driver.Core.Events</Namespace>
  <Namespace>MongoDB.Driver.Core.Events.Diagnostics</Namespace>
  <Namespace>MongoDB.Driver.Core.Misc</Namespace>
  <Namespace>MongoDB.Driver.Core.Operations</Namespace>
  <Namespace>MongoDB.Driver.Core.Operations.ElementNameValidators</Namespace>
  <Namespace>MongoDB.Driver.Core.Servers</Namespace>
  <Namespace>MongoDB.Driver.Core.WireProtocol</Namespace>
  <Namespace>MongoDB.Driver.Core.WireProtocol.Messages</Namespace>
  <Namespace>MongoDB.Driver.Core.WireProtocol.Messages.Encoders</Namespace>
  <Namespace>MongoDB.Driver.Core.WireProtocol.Messages.Encoders.BinaryEncoders</Namespace>
  <Namespace>MongoDB.Driver.Core.WireProtocol.Messages.Encoders.JsonEncoders</Namespace>
  <Namespace>MongoDB.Driver.GeoJsonObjectModel</Namespace>
  <Namespace>MongoDB.Driver.GeoJsonObjectModel.Serializers</Namespace>
  <Namespace>MongoDB.Driver.Linq</Namespace>
  <Namespace>Staw.DataModel</Namespace>
  <Namespace>System.Dynamic</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
  <Namespace>UtopiaPlanitia.DataModel</Namespace>
</Query>

void Main()
{
	var client = new MongoClient(new MongoClientSettings() {
		Server = new MongoServerAddress("localhost", 27017)
	});
	var db = client.GetDatabase("MyNewDb");
	
	StawData data = NewStawData(@"D:\git\StarTrekAttackWingApplication\STAW.POC.Testing\Resources\StawData.xml");
	//var reflect = new ReflectionUtility();
	foreach (var colname in data.CollectionNames)
	{
		db.CreateCollection(colname);
		db.
	}
	
	//var collection = db.GetCollection<BsonDocument>("Admirals");
	//showAll(collection);
	
}


public async void showAll(IMongoCollection<BsonDocument> collection) 
{
	var filter = new BsonDocument();
	var count = 0;
	using (var cursor = await collection.FindAsync(filter))
	{
		while (await cursor.MoveNextAsync())
		{
			var batch = cursor.Current;
			foreach (var document in batch)
			{
				// process document
				count++;
			}
		}
	}
	count.Dump();
}

public StawData NewStawData(string xmlFile)
{
	using (var stream = new FileStream(xmlFile, FileMode.Open))
	{
		XmlSerializer ser = new XmlSerializer(typeof(StawData));
		var obj = (StawData)ser.Deserialize(stream);
		stream.Dispose();
		return obj;
	}
}

public class ReflectionUtility
{
	public ReflectionUtility()
	{
	}

	public dynamic AddPropertyToDynamicFromResult(MethodInfo methodInfo, dynamic dynamic, string propertyName, Dictionary<string, object> namedParameters)
	{
		var result = methodInfo.IsStatic
			? CallMethod(methodInfo, namedParameters)
			: CreateInstanceAndCallMethod(methodInfo, namedParameters);

		AddPropertyToExpando(dynamic, propertyName, result);

		return dynamic;
	}

	public void AddPropertyToExpando(ExpandoObject expandoObject, string propertyName, object propertyValue)
	{
		var dictionary = (IDictionary<string, object>)expandoObject;
		dictionary.Add(propertyName, propertyValue);
	}

	public object CallMethod(MethodInfo methodInfo, IDictionary<string, object> namedParameters,
		object instance = null)
	{
		var parameters = MatchParameters(namedParameters, methodInfo.GetParameters());
		return CallMethod(methodInfo, instance, parameters.ToArray());
	}

	public object CallGenericMethod(MethodInfo methodInfo, object instance, Type genericType, object[] parameters)
	{
		var genericMethod = methodInfo.MakeGenericMethod(genericType);
		return genericMethod.Invoke(instance, parameters);
	}

	public object CallGenericMethod(string methodName, object instance, Type genericType, object[] parameters)
	{
		var methodInfo = GetMethodInfo(instance.GetType(), methodName);
		return CallGenericMethod(methodInfo, instance, genericType, parameters);
	}


	public object CallMethod(string methodName, object instance, object[] parameters)
	{
		var methodInfo = GetMethodInfo(instance.GetType(), methodName);
		return CallMethod(methodInfo, instance, parameters);
	}

	public object CallMethod(MethodInfo methodInfo, object instance, object[] parameters)
	{
		return methodInfo.Invoke(instance, parameters);
	}

	public object CallStaticMethod(string methodName, Type type, object[] parameters)
	{
		var methodInfo = GetMethodInfo(type, methodName);
		return CallStaticMethod(methodInfo, parameters);
	}

	public object CallStaticMethod(MethodInfo methodInfo, object[] parameters)
	{
		return methodInfo.Invoke(null, parameters);
	}


	public MethodInfo GetMethodInfo(string fullTypeName, string methodName)
	{
		var type = Type.GetType(fullTypeName);
		return GetMethodInfo(type, methodName);
	}

	public MethodInfo GetMethodInfo(object obj, string methodName)
	{
		var typeName = obj.GetType().AssemblyQualifiedName;
		return GetMethodInfo(typeName, methodName);
	}

	public MethodInfo GetMethodInfo(Type type, string methodName)
	{
		return type.GetMethod(methodName);
	}

	public object GetPropertyValue(object instance, string propertyName)
	{
		var type = instance?.GetType();
		var propertyInfo = type?.GetProperty(propertyName);
		return propertyInfo?.GetValue(instance);
	}

	public T GetPropertyValue<T>(object obj, string propertyName, T defaultValue)
	{
		var objectType = obj.GetType();
		var property = objectType.GetProperties().FirstOrDefault(p => p.Name == propertyName && p.PropertyType == typeof(T));
		var propertyValue = property?.GetValue(obj);
		return propertyValue != null ? (T)propertyValue : defaultValue;
	}

	public object CreateInstance(Type type, IDictionary<string, object> namedParameters)
	{
		var contstructors = type.GetConstructors();
		var contstructor = contstructors
			.Where(c => c.IsPublic)
			.OrderBy(c => c.GetParameters().Count()).First();

		var constructorParameterInfos = contstructor.GetParameters();
		var parametersToSendConstructor = MatchParameters(namedParameters, constructorParameterInfos);
		return contstructor.Invoke(parametersToSendConstructor);
	}

	private object CreateInstanceAndCallMethod(MethodInfo methodInfo, IDictionary<string, object> namedParameters)
	{
		var instance = CreateInstance(methodInfo.DeclaringType, namedParameters);
		return CallMethod(methodInfo, namedParameters, instance);
	}

	private object[] MatchParameters(IDictionary<string, object> namedParameters, IEnumerable<ParameterInfo> methodParameterInfos)
	{
		var callingParameters = new List<object>();

		foreach (var methodParameter in methodParameterInfos)
		{
			// Find parameters of matching type
			var possibleParameterTypes =
				namedParameters.Where(np => np.Value.GetType() == methodParameter.ParameterType.UnderlyingSystemType || GetAllBaseTypesAndInterfaces(np.Value.GetType()).Any(bt => bt == methodParameter.ParameterType.UnderlyingSystemType))
					.ToList();

			// If more than one match exists based on type, try then match on name
			var parameterValue = possibleParameterTypes.Count() == 1
				? possibleParameterTypes.First().Value
				: possibleParameterTypes.FirstOrDefault(p => p.Key == methodParameter.Name).Value;

			// If there is parameter matching name, but not type and we have a value to convert
			if (parameterValue == null && namedParameters.Any(np => np.Value != null && np.Key == methodParameter.Name))
			{
				var namedParameter = namedParameters.First(np => np.Key == methodParameter.Name);
				var methodParameterType = methodParameter.ParameterType.UnderlyingSystemType;
				// try to convert the value to the type of the matching named parameter
				try
				{
					var conversionType = !IsNullableType(methodParameterType)
						? methodParameter.ParameterType.UnderlyingSystemType
						: methodParameter.ParameterType.UnderlyingSystemType.GenericTypeArguments[0];
					parameterValue = ((IConvertible)namedParameter.Value).ToType(conversionType, null);
				}
				catch
				{
					// Eating this exception leaves the parameter value = null
				}
			}

			callingParameters.Add(parameterValue);
		}

		return callingParameters.ToArray();
	}

	private IEnumerable<Type> GetAllBaseTypesAndInterfaces(Type type)
	{
		var typeList = type.GetInterfaces().ToList();
		var baseType = type.BaseType;

		while (baseType != null)
		{
			typeList.Add(baseType);
			baseType = baseType.BaseType;
		}

		return typeList;
	}

	private bool IsNullableType(Type type)
	{
		// We can use any nullable type for comparision because we are just comparing the name
		return type.Name == typeof(int?).Name;
	}


	/// <summary>
	/// Get the count of a collection object, returns the Count property of a collection. 
	/// If the Count property is null(the object passed in is not a collection) then -1 is returned
	/// this is so GetCollectionCount doesn't break from something that is not a collection
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public int GetCollectionCount(object obj)
	{
		var type = obj?.GetType();

		var propertyNames = new string[]
		{
				"Count",
				"Length"
		};

		foreach (var propertyName in propertyNames)
		{
			var property = type?.GetProperty(propertyName);
			if (property != null)
			{
				return (int)property.GetValue(obj, null);
			}
		}

		return -1;
	}

	public string GetCollectionName(object obj)
	{
		var underlyingType = GetUnderlyingType(obj);
		
		return underlyingType.Name.Pluralize();
	}

	public bool IsEnumerable(Type entityType)
	{
		return entityType.GetInterfaces().Any(s => s == typeof(IEnumerable));
	}

	public bool IsCollection(Type type)
	{
		type.Name.Dump();
		return type.Name.Contains("Collection`1");
	}

	public bool IsEnumerable(object obj)
	{
		return obj is IEnumerable;
	}

	public Type GetUnderlyingType(object ent)
	{
		var type = ent.GetType();

		return type.IsGenericType ? type.GenericTypeArguments[0].UnderlyingSystemType : type;
	}

	/// <summary>
	/// Get a list of PropertyInfo from object named by one of the names
	/// </summary>
	/// <param name="names"></param>
	/// <returns></returns>
	public ICollection<PropertyInfo> PropertiesInObject(object obj, IEnumerable<string> names)
	{
		return obj.GetType().GetProperties().Where(p => names.Contains(p.Name)).ToList();
	}

	/// <summary>
	/// Gets the matching properties of a type based on the collection of MemberInfo
	/// </summary>
	/// <param name="type">The type to get properties from</param>
	/// <param name="members">The Collection of Members to check for when retrieving matching properties</param>
	/// <returns></returns>
	public ICollection<PropertyInfo> PropertiesInType(Type type, ICollection<MemberInfo> members)
	{
		var properties = type.GetProperties();

		return properties.Intersect(members).Select(s => ((PropertyInfo)s)).ToList();
	}
}