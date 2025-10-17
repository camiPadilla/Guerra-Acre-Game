using System.Collections.Generic;
using UnityEngine;
using TarodevController;

[System.Serializable]
public class GameData
{
    // Guarda la última posición conocida del jugador (por checkpoint)
    public float[] playerPosition = new float[3];

    // Lista de checkpoints activos
    public List<bool> checkpointsActivos = new List<bool>();

    // Lista de niveles completados
   // public List<PlayerData.LevelCompletionData> nivelesCompletados = new List<PlayerData.LevelCompletionData>();
}

[System.Serializable]
public class PlayerData
{
    public int vidasJugador;
    public int vidasExtras;
    public float[] position = new float[3];


    // Armas
    public int balas;
    public int tipoArma;

    public PlayerData(SaludPersonaje playerSalud, AtaquePersonaje playerAtaque, PlayerController player)
    {
        vidasJugador = playerSalud.vidasJugador;
        vidasExtras = playerSalud.vidasEXtras;
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        balas = playerAtaque.cantidadBalas;
        tipoArma = playerAtaque.seleccionArma;
    }
}

[System.Serializable]
public class ProgressData
{
    public int currentLevel;
    public List<int> checkPointsActivos = new List<int>();

    public ProgressData(int nivelActual, List<int> cpActivos)
    {
        currentLevel = nivelActual;
        checkPointsActivos = cpActivos;
    }
}

[System.Serializable]
public class LevelCompletionData
{
    public int levelIndex;
    public bool levelComplete;
    public int cantNotas;

    public LevelCompletionData(int levelIndex, bool levelComplete, int cantNotas)
    {
        this.levelIndex = levelIndex;
        this.levelComplete = levelComplete;
        this.cantNotas = cantNotas;
    }
}
