using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileManager
{
    public static void SaveToFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + $"/Player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        Save data = new Save();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Save LoadFromFile()
    {
        string path = Application.persistentDataPath + $"/Player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Save data = formatter.Deserialize(stream) as Save;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteSaveFile()
    {
        string path = Application.persistentDataPath + "/Player.save";
        if (File.Exists(path))
            File.Delete(path);
    }
}
