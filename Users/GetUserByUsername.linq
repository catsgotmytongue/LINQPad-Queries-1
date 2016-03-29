<Query Kind="Statements">
  <Connection>
    <ID>a33ccdf6-b147-48fb-84e0-2ce107b3ffbd</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>https://cenvapprd38.oya.ad/CoreDataService/CoreDataService.svc/</Server>
  </Connection>
</Query>

var username = "JJIS01";
// Gets the JJIS user and associated entity
JjisUsers
	.Expand(ju => ju.Entity)
	.Where(
		ju => ju.JjisUserHistories.Any(
			juh => juh.UserName == username
			&& juh.SecurityStartDate < DateTime.Now
			&& (juh.SecurityEndDate == null || juh.SecurityEndDate > DateTime.Now))
		)
.Dump();