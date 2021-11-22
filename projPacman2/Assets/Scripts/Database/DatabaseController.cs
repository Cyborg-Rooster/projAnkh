using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

class DatabaseController
{
    public static void Create(PlayerData data)
    {
        Database.Xml.Element("Database").Element("PlayersDatas").Add
        (
            new XElement
                    (
                        "PlayerData",
                        new XAttribute("id", data.ID),
                        new XAttribute("name", data.Name),
                        new XAttribute("value", data.Value)
                    )
        );

        Database.SaveDatabase();
    }

    public static PlayerData Read(string id)
    {
        PlayerData data = new PlayerData();
        var query = from p in Database.Xml.Descendants("PlayerData")
                    where (string)p.Attribute("id") == id
                    select p;

        foreach (var record in query)
        {

            data.ID = int.Parse(record.Attribute("id").Value);
            data.Name = record.Attribute("name").Value;
            data.Value = int.Parse(record.Attribute("value").Value);
        }

        return data;
    }

    public static void Update(PlayerData data)
    {
        var query = from p in Database.Xml.Descendants("PlayerData")
                    where (string)p.Attribute("id") == data.ID.ToString()
                    select p;

        foreach (XElement e in query)
        {
            e.SetAttributeValue("name", data.Name);
            e.SetAttributeValue("value", data.Value);
        }
        Database.SaveDatabase();
    }

    public static bool CheckIfExist(string id)
    {
        var query = from p in Database.Xml.Descendants("PlayerData")
                    where (string)p.Attribute("id") == id
                    select p;

        return query != null;
    }
}
