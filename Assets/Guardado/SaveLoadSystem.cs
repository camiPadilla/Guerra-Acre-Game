using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadSystem
{
    private static string basePath = Application.persistentDataPath + "/slot";

    public static void SaveGame(GameData data, int slot)
    {
        string path = basePath + slot + ".save";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Juego guardado en " + path);
    }

    public static GameData LoadGame(int slot)
    {
        string path = basePath + slot + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            Debug.Log("Juego cargado desde " + path);
            return data;
        }
        else
        {
            Debug.LogWarning("No hay guardado en el slot " + slot);
            return null;
        }
    }

    public static void DeleteSlot(int slot)
    {
        string path = basePath + slot + ".save";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Guardado del slot " + slot + " eliminado.");
        }
    }

    public static bool HasSave(int slot)
    {
        string path = basePath + slot + ".save";
        return File.Exists(path);
    } 
    public static void DeleteAllData()
    {
        for(int i = 0; i<=3; i++)
        {
            string path = basePath + 1 + ".save";
            if(File.Exists(path))
                File.Delete(path);
        }
        Debug.Log("Adios partida :(");
    }
}




