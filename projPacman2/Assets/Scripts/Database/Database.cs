using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

class Database
{
    static readonly string directory = $@"{Application.dataPath}\Database";

    public static XDocument Xml { get; set; }

    public static bool CheckifXMLExist()
    {
        if (Directory.Exists(directory))
        {
            Xml = XDocument.Load(directory + @"\Database.xml");
            return true;
        }
        return false;
    }

    public static void CreateDatabase()
    {
        Directory.CreateDirectory(directory);
        Console.WriteLine("Database dont't exist. Creating...");

        XDocument x = new XDocument
            (
                new XDeclaration("1.0", "UTF-16", "yes"),
                new XElement("Database", 
                    new XElement("PlayersDatas",
                        CreateArtificialData("MATHEUS", "10000", "0"),
                        CreateArtificialData("JULIA", "9000", "1"),
                        CreateArtificialData("CARLOS", "8000", "2"),
                        CreateArtificialData("VITOR", "7000", "3"),
                        CreateArtificialData("HELENA", "6000", "4"),
                        CreateArtificialData("MARCOS", "4000", "5"),
                        CreateArtificialData("MAYARA", "3000", "6"),
                        CreateArtificialData("JOOJ", "2000", "7")))
            );

        Xml = x;
        SaveDatabase();
    }

    public static void SaveDatabase()
    {
        Xml.Save(directory + @"\Database.xml");
    }

    private static XElement CreateArtificialData(string name, string value, string id)
    {
        return new XElement
        (
            "PlayerData",
            new XAttribute("id", id),
            new XAttribute("name", name),
            new XAttribute("value", value)
        );
    }
}
