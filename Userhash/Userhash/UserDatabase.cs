using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[Serializable]
[XmlRoot("UserDatabase")] // Přidáme kořenový element pro serializaci
public class UserDatabase
{
    [XmlElement("User")] // Přidáme tento atribut pro každý uživatelský prvek v seznamu
    public List<User> Users { get; set; } = new List<User>();

    private const string FilePath = "users.xml";

    public void Save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UserDatabase));
        using (StreamWriter writer = new StreamWriter(FilePath))
        {
            serializer.Serialize(writer, this); // Serializace nyní s definovaným kořenovým prvkem
        }
    }

    public static UserDatabase Load()
    {
        if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
        {
            UserDatabase db = new UserDatabase();
            db.Users.Add(new Admin("admin", "admin")); // Default admin
            db.Save();
            return db;
        }

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserDatabase));
            using (StreamReader reader = new StreamReader(FilePath))
            {
                return (UserDatabase)serializer.Deserialize(reader);
            }
        }
        catch (InvalidOperationException)
        {
            // Pokud dojde k chybě při deserializaci, například prázdný soubor, obnovíme původní stav
            UserDatabase db = new UserDatabase();
            db.Users.Add(new Admin("admin", "admin")); // Default admin
            db.Save();
            return db;
        }
    }
}
