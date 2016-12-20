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
//	var a = "Referrals($expand=Allegations($expand=AllegationStates($expand=Disposition,DecisionPointAllegationStates))),"
//	   + "Referrals($expand=Allegations($expand=AllegationStates($expand=Prop1, Prop2, Property3)))";
	   var a = @"YouthAssessments($expand=Assessment         
	   		($expand=AssesmentPointMeasures($expand=AssessmentPoint($expand=MeasurementTypeCodeItem),AssessmentPointAnswers),AssessmentTool($expand=AssessmentPoints($expand=MeasurementTypeCodeItem)))),DecisionPoints($expand=DecisionPointAllegationStates($expand=AllegationState)),Referrals($expand=Allegations($expand=AllegationStates($expand=Allegation,Disposition($expand=DispositionDefinition))))";
	var r = ParseExpandExpr(a);
	
	//r.Dump();
	BuildNavPaths(r).Dump();
	
}
string pathSep = "/";
string[] BuildNavPaths(ExpandTreeNode treeRoot)
{
	if (!treeRoot.ChildNodes.Any())
		return new string[] {treeRoot.Identifier};
	
	var navPaths = new List<string>();

	foreach (var cn in treeRoot.ChildNodes)
	{
		var paths = BuildNavPaths(cn);
		foreach (var p in paths)
		{
			navPaths.Add(treeRoot.Identifier+pathSep+p);
		}
	}
	
	return navPaths.ToArray();
}

//private bool IsWhiteSpace(char c) => " \n\r\t".Contains(c); 

// Define other methods and classes here
ExpandTreeNode ParseExpandExpr(string expandStr)
{
	// build our own parser... 
	var expandTree = new List<ExpandTreeNode>();
	var identifierString = new StringBuilder();
	Stack<ExpandTreeNode> previousScopeNodes = new Stack<ExpandTreeNode>();
	
	var currentNode = new ExpandTreeNode {Identifier = "", ChildNodes = new List<ExpandTreeNode>()};
	
	var Root = currentNode;
	
	for (int i = 0; i< expandStr.Length; i++)
	{
		var c = expandStr[i];

		if( c.IsWhiteSpace() ) continue;

		if (c != '(' && c != ')' && c != '$' && c != '=' && c != ',')
		{ 
			identifierString.Append(c);
			// advance
			continue;
		}

		if (c == '(')
		{
			// save the current scope for later
			previousScopeNodes.Push(currentNode);

			// declaring a new scope
			
			var newSyntaxNode = new ExpandTreeNode { Identifier = identifierString.ToString(), ChildNodes = new List<ExpandTreeNode>()};
			currentNode.ChildNodes.Add(newSyntaxNode);
			currentNode = newSyntaxNode;
			
			//identifierString.ToString().Dump();
			identifierString.Clear();
			
			continue;
		}

		// if we have a dollar sign speed past the expand token
		if (c == '$')
		{
			while ( !identifierString.ToString().Equals("expand") ) {   
				i++; 
				c = expandStr[i];
				identifierString.Append(c); 
			}
			identifierString.Clear();
			continue;
		}

		if (c == ',')
		{
			if(!String.IsNullOrEmpty(identifierString.ToString()))
				currentNode.ChildNodes.Add(new ExpandTreeNode { Identifier = identifierString.ToString(), ChildNodes = new List<ExpandTreeNode>()});
						
			identifierString.Clear();
			continue;
		}
		
		if (c == ')')
		{
			// ending an expand scope
			if(!String.IsNullOrEmpty(identifierString.ToString()))
				currentNode.ChildNodes.Add(new ExpandTreeNode { Identifier = identifierString.ToString(), ChildNodes = new List<ExpandTreeNode>()});

			currentNode = previousScopeNodes.Pop();
			
			identifierString.Clear();
			continue;
		}
		
		
	}
	
	return Root;
}

public class ExpandTreeNode
{
	public string Identifier { get; set; }
	public List<ExpandTreeNode> ChildNodes { get; set; }
}