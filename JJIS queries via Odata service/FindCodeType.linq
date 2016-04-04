<Query Kind="Statements">
  <Connection>
    <ID>a33ccdf6-b147-48fb-84e0-2ce107b3ffbd</ID>
    <Driver>AstoriaAuto</Driver>
    <Server>https://cenvapprd38.oya.ad/CoreDataService/CoreDataService.svc/</Server>
    <DisplayName>Development Data Service</DisplayName>
  </Connection>
</Query>

var source = Placements
			.Select(x => new { CodeValue = x.PlacementTypeCode})
			.Take(500)
			.ToList()
			.Distinct();

var codes = Codes;

var relatedCodes =
	from x in source
	join c in codes on x.CodeValue equals c.CodeValue
	orderby c.Type, c.CodeValue 
	select new { c.Type, c.CodeValue, c.Description};
 
var groupedCodes = 
	from r in relatedCodes
	group r by r.Type into g
	orderby g.Count() descending
	select new { g.Key, Count = g.Count() };

var likelyType = groupedCodes.First()?.Key;

codes.Where(c => c.Type == "CODT" && c.CodeValue == likelyType).Dump();
codes.Where(c => c.Type == likelyType).Dump();


groupedCodes.Dump();
relatedCodes.Dump();