using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveLoadSystem
{
   public static void CreateLevelData(int currentWorld, int lastWorld, List<WorldCompletionData> worldData, int correctTotal, int incorrectTotal)
   {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath + "/lvlData.lud");
        FileStream stream = new FileStream(path, FileMode.Create);

        ProgressData newData = new ProgressData(currentWorld, lastWorld, worldData, correctTotal, incorrectTotal);

        formatter.Serialize(stream, newData);
        stream.Close();
   }

    public static void SaveLevelData(ProgressData newData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath + "/lvlData.lud");
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, newData);
        stream.Close();
    }

    public static ProgressData LoadLevelData()
   {
        string path = Path.Combine(Application.persistentDataPath + "/lvlData.lud");

        //Debug.Log(Application.persistentDataPath+path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ProgressData newData = formatter.Deserialize(stream) as ProgressData;
            //Debug.Log("datos de mundo -> " +  newData.worldData.Count);
            stream.Close();

            return newData; 
        }
        else
        {
            Debug.LogWarning("Can't find save file in path " + path + " creating");
            return null;
        }
    }
/*
    public static void SaveInventoryData(EquipmentData newInventoryData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath + "/invData.lud");
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, newInventoryData);
        stream.Close();
    }

    public static EquipmentData LoadInventoryData()
    {
        string path = Path.Combine(Application.persistentDataPath + "/invData.lud");

        //Debug.Log(Application.persistentDataPath+path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EquipmentData newData = formatter.Deserialize(stream) as EquipmentData;
            //Debug.Log("datos de mundo -> " +  newData.worldData.Count);
            stream.Close();

            return newData;
        }
        else
        {
            Debug.LogWarning("Can't find save file in path " + path + " creating");
            return null;
        }
    }*/
}
