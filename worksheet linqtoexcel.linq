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
	var fileBaseName = "Mar2017";
	var exc = new ExcelQueryFactory($@"c:\temp\{fileBaseName}.xlsx");

	var worksheetEntries = exc.Worksheet<Entry>(fileBaseName);

	var worksheetEntriesGrouped = worksheetEntries.ToList().GroupBy(c => c.Date).OrderBy(c => c.Key.Date);
	//worksheetEntriesGrouped.Dump();

	var balances = new List<DateBalance>();
	var amountsPerDate = new Dictionary<int, DatedEntry>();
	foreach (var group in worksheetEntriesGrouped)
	{
		var key = group.Key.Date;
		var orderedByBalance = group.OrderBy(g => g.TransactionOrder);

		var entries = "=" + String.Join("+", orderedByBalance.Where(d => d.Category == "Random").Select(c => -1 * c.Amount));
		var bills = "" + String.Join($"; ", orderedByBalance.Where(d => d.Category != "Random").GroupBy(c => c.Category, k => k.Amount).Select(c => $"{c.Key} = {String.Join("+", c.Select(c2 => c2 * -1))}"));


		//key.Dump();
		balances.Add(new DateBalance{ Date = key, Balance = orderedByBalance.Last().Balance });
		amountsPerDate.Add(key.Day, new DatedEntry { Date = key, Random = entries, Bills = bills, EndBalance = orderedByBalance.Last().Balance });
	}

	balances.Select(b=>b.Balance).Dump("Balances");
	double prevBalance = 0;
	Enumerable.Range(1, 31)
	.Select(day =>
	{
		prevBalance = amountsPerDate.ContainsKey(day)? amountsPerDate[day].EndBalance : prevBalance;
		
	return amountsPerDate.ContainsKey(day)
	? new DayEntry { Day = day, Random = amountsPerDate[day].Random, Bills = amountsPerDate[day].Bills, EndBalance = amountsPerDate[day].EndBalance }
	: new DayEntry { Day = day, Random = "", Bills = "", EndBalance = prevBalance };

	})
	.Dump();
	
	//amountsPerDate.Dump();
}

public class DateBalance
{
	public DateTime Date { get; set; }
	public double Balance { get; set;}
}

public class DatedEntry
{
	public DateTime Date { get; set; }
	public Object Random { get; set; }
	public Object Bills { get; set; }

	public double EndBalance { get; set; }
}
public class DayEntry
{
	public int Day { get; set; }
	public Object Random { get; set; }
	public Object Bills { get; set; }
	public double EndBalance { get; set; }
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