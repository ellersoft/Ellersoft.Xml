module Ellersoft.Xml
module Array =
    open System.Xml

    let ofXmlNodeList (nodes : XmlNodeList) =
        let arr = Array.zeroCreate nodes.Count
        for i in 0..nodes.Count - 1 do arr.[i] <- nodes.Item(i)
        arr
        
    let ofXmlAttributeCollection (attrs : XmlAttributeCollection) =
        let arr = Array.zeroCreate attrs.Count
        for i in 0..attrs.Count - 1 do arr.[i] <- attrs.Item(i)
        arr

open System.Xml

let readXml (xml : string) =
    let doc = XmlDocument()
    xml |> doc.LoadXml
    doc

let getValueOrText (node : XmlNode) = match node.Value, node.InnerText with | null, v -> v | v, _ -> v
let getXml (node : XmlNode) = node.InnerXml
let getText (node : XmlNode) = node.InnerText
let getValue (node : XmlNode) = node.Value
let getName (node : XmlNode) = node.Name
let childNodes (parent : XmlNode) = parent.ChildNodes |> Array.ofXmlNodeList
let selectNodesByXPath (xpath : string) (parent : XmlNode) = xpath |> parent.SelectNodes |> Array.ofXmlNodeList 
    
let getAttributeByName (name : string) (node : XmlNode) =
    node.Attributes |> Array.ofXmlAttributeCollection |> Array.tryFind (fun x -> x.Name = name)
    
let filterNodes (filter : XmlNode -> bool) (parent : XmlNode) =
    parent |> childNodes |> Array.filter filter
    
let filterNodesByName (name : string) (parent : XmlNode) =
    parent |> filterNodes (fun x -> x.Name = name)
    
let firstNodeByName (name : string) (parent : XmlNode) =
    parent |> filterNodesByName name |> Array.tryHead
