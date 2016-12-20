<Query Kind="Program">
  <Connection>
    <ID>9d4dd871-8ecd-429e-82e8-53015c38f33d</ID>
    <Persist>true</Persist>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="b0226d64cf8bfba4">OData4.OData4DynamicDriver</Driver>
    <DriverData>
      <Uri>https://services-test.jjis.oya.ad/CoreDataService/odata</Uri>
      <AuthenticationType>1</AuthenticationType>
    </DriverData>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>LinqToExcel</NuGetReference>
  <NuGetReference>log4net</NuGetReference>
  <Namespace>LinqToExcel</Namespace>
  <Namespace>LinqToExcel.Attributes</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
</Query>

IEnumerable<JjisNetDataLink> AllExistingDatalinks;
IEnumerable<JjisNetDataLinkExcelRecord> ExcelDataLinkRecords;

void Main()
{
	try
	{
		var sourceExcelFile = "http://oyanet.oya.state.or.us/BusinessServices/InformationSystems/Document%20Library/JJIS%20Datalinks.xlsx";
		var downloadFileName = @"c:\temp\netdatalinks.xlsx";

		SaveFileFromWeb(sourceExcelFile, downloadFileName);

		ExcelDataLinkRecords = GetDatalinkRecordsFromTab(downloadFileName, "Word Datalinks")
								.Where(r => !string.IsNullOrEmpty(r.Identifier)
											&& !string.IsNullOrEmpty(r.Expression))
							.ToList();

		AllExistingDatalinks = JjisNetDataLinks.ToList();

		// Check for duplicate datalink records
		CheckForDuplicateDataLinks();

		// Delete Datalinks that have been removed from the excel spreadsheet
		DeleteDataLinksNotInExcel();

		// Update or insert records from spreadsheet
		UpdateDatalinksRecords();
		
		"\nAll Datalinks".Dump();
		JjisNetDataLinks.Dump();
	}
	catch (Exception ex)
	{
		ex.Dump();
	}
}


public void CheckForDuplicateDataLinks()
{
	var Identifiers = new List<string>();
	var duplicatedIdentifiers = new List<string>();
	foreach (var excelRecord in ExcelDataLinkRecords)
	{
		if (Identifiers.Contains(excelRecord.Identifier))
		{
			duplicatedIdentifiers.Add(excelRecord.Identifier);
			excelRecord.Identifier = GetUniqueIdentifier(excelRecord.Identifier, Identifiers);
		}

		Identifiers.Add(excelRecord.Identifier);
	}

	// Some records that may need some work
	"Duplicate Datalinks".Dump();
	ExcelDataLinkRecords.Where(i => duplicatedIdentifiers.Any(d => d == i.Identifier)).OrderBy(i => i.Identifier).Dump();
}

public void UpdateDatalinksRecords()
{
	var createdDatalinks = new List<JjisNetDataLink>();
	foreach (var excelRecord in ExcelDataLinkRecords)
	{
		try
		{
			var existingDatalink = AllExistingDatalinks.FirstOrDefault(dl => dl.Identifier == excelRecord.Identifier);
			if (existingDatalink != null)
			{
				UpdateExistingDatalink(excelRecord, existingDatalink);
			}
			else
			{
				var newDatalink = CreateNewDataLink(excelRecord);
				createdDatalinks.Add(newDatalink);
			}
		}
		catch (Exception ex)
		{
			// We should only get here if we have some invalid data in our Excel file.
			ex.Dump();
		}
	}
	SaveChangesBatchContinueOnError();
	"\nThe following datalinks were added".Dump();
	createdDatalinks.Dump();
}

public JjisNetDataLink CreateNewDataLink(JjisNetDataLinkExcelRecord excelRecord)
{
	var newDataLink = new JjisNetDataLink();
	CopyDataFromExcelRecordToJjisNetDatalink(excelRecord, ref newDataLink);
	AddToJjisNetDataLinks(newDataLink);
	return newDataLink;
}

public void UpdateExistingDatalink(JjisNetDataLinkExcelRecord excelRecord, JjisNetDataLink jjisNetDatalink)
{
	CopyDataFromExcelRecordToJjisNetDatalink(excelRecord, ref jjisNetDatalink);
	UpdateObject(jjisNetDatalink);
}

public void CopyDataFromExcelRecordToJjisNetDatalink(JjisNetDataLinkExcelRecord excelRecord, ref JjisNetDataLink jjisNetDatalink)
{
	long jjisDatalinkId = 0;
	Int64.TryParse(excelRecord.JjisDatalinkId, out jjisDatalinkId);

	jjisNetDatalink.Description = excelRecord.Description;
	jjisNetDatalink.EntityClassName = excelRecord.EntityClassName;
	jjisNetDatalink.Expression = excelRecord.Expression;
	jjisNetDatalink.Identifier = excelRecord.Identifier;
	jjisNetDatalink.Name = excelRecord.DisplayName.TrimLengthTo(50);
	jjisNetDatalink.ReturnTypeCode = excelRecord.ReturnTypeCode;
	jjisNetDatalink.ReturnDomain = excelRecord.ReturnDomain;
	jjisNetDatalink.ExpandPath = excelRecord.ExpandPath;
	jjisNetDatalink.LegacyDataLinkId = jjisDatalinkId != 0 ? (long?)jjisDatalinkId : null;
	jjisNetDatalink.ParentCategory = !string.IsNullOrEmpty(excelRecord.ParentCategory) ? excelRecord.ParentCategory : excelRecord.EntityClassName;
	jjisNetDatalink.VisibleFlagAsString = !string.IsNullOrEmpty(excelRecord.VisibleFlag) ? excelRecord.VisibleFlag : "Y";
}

public void DeleteDataLinksNotInExcel()
{
	var deletedDataLinks = new List<JjisNetDataLink>();
	foreach (var dataLink in AllExistingDatalinks)
	{
		if (!ExcelDataLinkRecords.Any(edr => edr.Identifier == dataLink.Identifier))
		{
			DeleteObject(dataLink);
			deletedDataLinks.Add(dataLink);
		}
	}

	"\nThe following datalinks were deleted".Dump();
	deletedDataLinks.Dump();
	
	SaveChangesBatchContinueOnError();
}

public void SaveChangesBatchContinueOnError()
{
	try
	{
		SaveChanges(SaveChangesOptions.BatchWithSingleChangeset);
	}
	catch
	{
		try
		{
			SaveChanges(SaveChangesOptions.ContinueOnError);
		}
		catch (Exception ex)
		{
			ex.Dump();
		}
	}
}


public string GetUniqueIdentifier(string identifier, ICollection<string> identifiers)
{
	var newIdentifier = identifier;
	var number = 1;

	while (identifiers.Any(i => i == newIdentifier))
	{
		newIdentifier = $"{identifier}_duplicate{number}";
		number++;
	}

	return newIdentifier;
}

public class JjisNetDataLinkExcelRecord
{
	private static int DisplayNameIndex = 0;
	private static int LongDescriptionIndex = 1;
	private static int UniqueIdentifierIndex = 2;
	private static int CSharpExpressionIndex = 3;
	private static int ReturnTypeCodeIndex = 4;
	private static int NetEntityClassNameIndex = 5;
	private static int ParentCategoryIndex = 6;
	private static int ReturnDomainIndex = 7;
	private static int ExpandPathIndex = 8;
	private static int VisibleFlagIndex = 9;
	private static int JJISDatalinkIdIndex = 10;
	private static int SmalltalkMessageIndex = 11;
	private static int TestStatusIndex = 12;

	public JjisNetDataLinkExcelRecord(LinqToExcel.Row r)
	{
		DisplayName = r[DisplayNameIndex];
		EntityClassName = r[NetEntityClassNameIndex];
		Expression = r[CSharpExpressionIndex];
		Identifier = r[UniqueIdentifierIndex];
		JjisDatalinkId = r[JJISDatalinkIdIndex];
		ReturnDomain = r[ReturnDomainIndex];
		ExpandPath = r[ExpandPathIndex];
		ReturnTypeCode = r[ReturnTypeCodeIndex];
		SmallTalkMessge = r[SmalltalkMessageIndex];
		TestStatus = r[TestStatusIndex];
		Description = r[LongDescriptionIndex];
		ParentCategory = r[ParentCategoryIndex];
		VisibleFlag = r[VisibleFlagIndex];
	}

	public string DisplayName;

	public string Description;

	public string Identifier;

	public string Expression;

	public string ReturnTypeCode;

	public string EntityClassName;

	public string ReturnDomain;

	public string ExpandPath;

	public string JjisDatalinkId;

	public string SmallTalkMessge;

	public string TestStatus;

	public string ParentCategory;

	public string VisibleFlag;
}

public IEnumerable<JjisNetDataLinkExcelRecord> GetDatalinkRecordsFromTab(string excelFilename, string tabName)
{
	var excel = new ExcelQueryFactory(excelFilename);

	excel.ReadOnly = true;

	var worksheetNames = excel.GetWorksheetNames();

	if (!worksheetNames.Contains(tabName))
		throw new InvalidOperationException($"{tabName} not found in document");



	return excel.Worksheet(tabName).Select(c => new JjisNetDataLinkExcelRecord(c));

}

// Define other methods and classes here
public void SaveFileFromWeb(string uriToFile, string dlFilename)
{
	using (var handler = new HttpClientHandler { UseDefaultCredentials = true })
	using (var webClient = new HttpClient(handler))
	{
		var bytes = webClient.GetByteArrayAsync(uriToFile);

		System.IO.File.WriteAllBytes(dlFilename, bytes.Result);
	}

}

public static class Extensions
{
	public static string TrimLengthTo(this string str, int length)
	{
		return str.Length > length ? str.Substring(0, length) : str;
	}
}