using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
    public static void CreateLevelData()
    {

    }
    public static void SaveLevelData()
    {

    }
    public static GameData LoadLevelData()
    {
        string path = Application.persistentDataPath + "/leveldata.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("No se ha encontrado el archivo de guardado en " + path);
            return null;
        }
    }
}
