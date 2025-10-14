using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
    public static void CreateLevelData() 
    //al crear nueva partida, incluir daros del nivel, lo terminado que esta y eso, supongo que solo eso , si se me ocurre mas, agregar mas
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/leveldata.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData();

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveLevelData()
    {
        //progreso del nivel, ademas de los datos del jugador, el checkpoint en el que esta, y si ha terminado el nivel o no, incluso las notas recogidas
        //supongo que por partida , son equis notas
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/leveldata.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData();

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static GameData LoadLevelData()
    {
        //lo mismo que saveleveldata, pero para cargar
        //wanna guess )= ya me olvide 
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
