<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.dll</Reference>
  <Namespace>Microsoft.Build.Construction</Namespace>
  <Namespace>Microsoft.Build.Debugging</Namespace>
  <Namespace>Microsoft.Build.Evaluation</Namespace>
  <Namespace>Microsoft.Build.Exceptions</Namespace>
  <Namespace>Microsoft.Build.Execution</Namespace>
  <Namespace>Microsoft.Build.Logging</Namespace>
</Query>

void Main()
{
//ProjectCollection.GlobalProjectCollection.UnloadAllProjects();
    //var propertiesGlobal = ;
	
	var p = new Project(@"C:\dev\TFS\Core\Trunk\Core.Utility\Core.Utility.csproj", new Dictionary<string,string>() { 
		{"Configuration","Release"} 
	},"12.0");
	
	//new Microsoft.Build.Evaluation.Project(serverBuildProjectItem, new Dictionary<string,string>() { 		{"Configuration",platformConfiguration} 	},"12.0");
	//p.ToolsVersion.Dump();
	var conditioned = p.ConditionedProperties.OrderBy(d=>d.Key);//.Dump();
	var c = p.AllEvaluatedProperties.OrderBy(f=>f.Name).Where(d=>d.Name.Equals("BuildOutputType"));
	//c.Dump(1);
	ProjectCollection.GlobalProjectCollection.UnloadProject(p);
}

// Define other methods and classes here
