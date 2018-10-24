<Query Kind="FSharpProgram">
  <NuGetReference>FSharp.Data</NuGetReference>
</Query>

open FSharp.Data


let duration f = 
    let timer = new System.Diagnostics.Stopwatch()
    timer.Start()
    let returnValue = f()
    printfn "Elapsed Time: %i" timer.ElapsedMilliseconds
    returnValue
    
let (|Int|_|) str =
   match System.Int32.TryParse(str) with
   | true, a -> Some(a)
   | false, _ -> None


type stawType = XmlProvider<"D:/Dev/temp/staw_data.xml">
let starTrekCards = stawType.Load(@"D:/Dev/temp/staw_data.xml")

let admirals = starTrekCards.Admirals

type ShipId = 
    | ShipId of int option
    | ShipIdString of string
    
type ShipClassId = ShipClassId of int
type Maneuver = {speed: int; kind: string; color: string} 


type shipCard = {ShipId: ShipId; ShipTitle: string; ShipClass: string; Moves: Maneuver []; Factions: string; AlternativeFaction: string option} 

//type GetShipCard = stawType.Ship -> shipCard


let shipTitle x = x.ShipTitle

let getShipClassDetail shipClassName = 
    starTrekCards.ShipClassDetails 
    |> Seq.find (fun x-> x.Name = shipClassName)
    
//let shipDetail id =
//    starTrekCards.Ships 
//    |> Seq.find (fun x -> x.Id = id )
 
let shipIdOf (s: stawType.Id2) =
    match s.String with
    | Some(Int i) -> ShipId(Some i)
    | None -> ShipIdString s.String.Value
    | Some(s2) -> ShipIdString s2

let getShipCard (ship: stawType.Ship) = 
    let shipClassDetail = getShipClassDetail ship.ShipClass
    {   ShipId= shipIdOf ship.Id;        
        ShipTitle = ship.Title; 
        ShipClass = shipClassDetail.Name; 
        Moves = shipClassDetail.Maneuvers 
                |> Array.map (fun x -> {speed = x.Speed; kind = x.Kind; color = x.Color});
        Factions = ship.Faction; AlternativeFaction = ship.AdditionalFaction
         }
        
let fullShipCard id =
    starTrekCards.Ships |> Seq.find (fun x -> x.Id = id) |> getShipCard
        
let shipsByClass cls = 
    starTrekCards.Ships 
    |> Seq.filter (fun x -> x.ShipClass = cls)
    |> Seq.map getShipCard


//shipsByClass "Galaxy Class" |> Seq.distinctBy shipTitle 
starTrekCards.Ships |> Seq.map getShipCard |> Seq.sortByDescending (fun x -> x.AlternativeFaction)
|> Dump |> ignore
