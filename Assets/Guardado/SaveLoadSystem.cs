using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;
using TarodevController;

public static class SaveLoadSystem
{
    private static string playerPath = Application.persistentDataPath + "/playerData.save";
    private static string gamePath = Application.persistentDataPath + "/gameData.save";
    public static void SavePlayerData(SaludPersonaje playerSalud, AtaquePersonaje playerAtaque, PlayerController playerController)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(playerPath, FileMode.Create);

        PlayerData data = new PlayerData(playerSalud, playerAtaque, playerController);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Datos del jugador guardados");
    }
    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(playerPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(playerPath, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Debug.Log("Datos del jugador cargados");
            return data;
        }
        else
        {
            Debug.LogWarning("no hay archivos de guardado del jugador");
            return null;
        }
    }

    public static void SaveLevelData(GameData gameData)
    {
        if (gameData == null)
        {
            Debug.LogError("No hay GameData para guardar");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(gamePath, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();

        Debug.Log(" Progreso del juego guardado");
    }
    public static GameData LoadLevelData()
    {
        if (File.Exists(gamePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(gamePath, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            Debug.Log(" Datos del progreso del juego cargados");
            return data;
        }
        else
        {
            Debug.LogWarning("No se encontró archivo de guardado del juego.");
            return null;
        }
    }


    public static void DeleteAllData()
    {
        if (File.Exists(playerPath)) File.Delete(playerPath);
        if (File.Exists(gamePath)) File.Delete(gamePath);
        Debug.Log("Se eliminaron todos los datos de guardado.");
    }
}
