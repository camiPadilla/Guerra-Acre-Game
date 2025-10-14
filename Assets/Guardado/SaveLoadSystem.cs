using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
    private static string levelDataPath = Application.persistentDataPath + "/leveldata.fun";
    private static string playerDataPath = Application.persistentDataPath + "/playerdata.fun";

    public static void CreateLevelData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(levelDataPath, FileMode.Create);

        GameData data = new GameData(); // de momento vacío
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveLevelData(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(levelDataPath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadLevelData()
    {
        if (File.Exists(levelDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(levelDataPath, FileMode.Open);

            GameData data = (GameData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("No se ha encontrado el archivo de guardado en " + levelDataPath);
            return null;
        }
    }

    public static void DeleteLevelData()
    {
        if (File.Exists(levelDataPath))
            File.Delete(levelDataPath);
    }
    public static void SavePlayerData(SaludPersonaje salud, AtaquePersonaje ataque, PlayerController playerCon)
    {
        PlayerData playerData = new PlayerData(salud, ataque, playerCon);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(playerDataPath, FileMode.Create);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(playerDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(playerDataPath, FileMode.Open);

            PlayerData playerData = (PlayerData)formatter.Deserialize(stream);
            stream.Close();
            return playerData;
        }
        else
        {
            Debug.LogError("No se ha encontrado el archivo de guardado en " + playerDataPath);
            return null;
        }
    }
}
