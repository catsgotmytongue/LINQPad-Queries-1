<Query Kind="Program">
  <NuGetReference>LinqToExcel</NuGetReference>
  <NuGetReference>log4net</NuGetReference>
  <Namespace>LinqToExcel</Namespace>
  <Namespace>LinqToExcel.Attributes</Namespace>
  <Namespace>LinqToExcel.Domain</Namespace>
  <Namespace>LinqToExcel.Extensions</Namespace>
  <Namespace>LinqToExcel.Query</Namespace>
  <Namespace>log4net</Namespace>
  <Namespace>log4net.Appender</Namespace>
  <Namespace>log4net.Config</Namespace>
  <Namespace>log4net.Core</Namespace>
  <Namespace>log4net.DateFormatter</Namespace>
  <Namespace>log4net.Filter</Namespace>
  <Namespace>log4net.Layout</Namespace>
  <Namespace>log4net.Layout.Pattern</Namespace>
  <Namespace>log4net.ObjectRenderer</Namespace>
  <Namespace>log4net.Plugin</Namespace>
  <Namespace>log4net.Repository</Namespace>
  <Namespace>log4net.Repository.Hierarchy</Namespace>
  <Namespace>log4net.Util</Namespace>
  <Namespace>log4net.Util.TypeConverters</Namespace>
</Query>

void Main()
{
	var fileBaseName = "Oct2016";
	var exc = new ExcelQueryFactory($@"c:\temp\{fileBaseName}.xlsx");

	var worksheetEntries = exc.Worksheet<Entry>($"Sheet1");
	
	var worksheetEntriesGrouped = worksheetEntries.ToList().GroupBy(c=>c.Date).OrderBy(c => c.Key.Date);
	worksheetEntriesGrouped.Dump();

	var balances = new List<object>();
	var amountsPerDate = new List<object>();
	foreach (var group in worksheetEntriesGrouped)
	{		
		var key = group.Key.Date.ToString("d");
		var orderedByBalance = group.OrderBy(g => g.TransactionOrder);

		var entries = "=" + String.Join("+", orderedByBalance.Where(d => d.Category == "Random"||d.Category == "Misc").Select(c => -1 * c.Amount));
		var bills = "" + String.Join($"; ", orderedByBalance.Where(d => d.Category != "Random" && d.Category != "Misc").GroupBy(c => c.Category, k => k.Amount).Select(c => $"{c.Key} = {String.Join("+",c.Select(c2=>c2*-1))}"));


		key.Dump();
		balances.Add(new { Date = key, Balance = orderedByBalance.Last().Balance });
		amountsPerDate.Add(new { Date = key, Random = entries, Bills = bills});
	}
	
	balances.Dump();
	amountsPerDate.Dump();
}

// Define other methods and classes here
public class Entry
{
	public DateTime Date { get; set; }
	public string Description { get; set; }
	public double Amount { get; set; }
	public double Balance { get; set; }
	public int TransactionOrder { get; set; }
	public string Category { get; set; }
	public string TransactionType { get; set; }
}