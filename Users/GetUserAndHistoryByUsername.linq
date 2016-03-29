<Query Kind="Statements">
  <Connection>
    <ID>a33ccdf6-b147-48fb-84e0-2ce107b3ffbd</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>https://cenvapprd38.oya.ad/CoreDataService/CoreDataService.svc/</Server>
  </Connection>
</Query>

var username = "JJIS01";

// Gets the current user history record and associations of interest
JjisUserHistories
				.Expand(juh => juh.JjisUser.Entity)
				.Where(
					juh => juh.UserName == username
					&& juh.SecurityStartDate < DateTime.Now
					&& (juh.SecurityEndDate == null || juh.SecurityEndDate > DateTime.Now))
				.OrderBy(juh => juh.SecurityStartDate)
				.First()

// To output in LINQPad
.Dump();



