using System;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

[Serializable]
public class GameData
{
    public int slotNumber;

    public float[] position = new float[3];

    public int vidasJugador;
    public int vidasExtras;
    public int balas;
    public int tipoArma;

    public int currentLevel;
    public string lastScene;
    public string lastSceneName;

    public int lastCheckPoint = -1; 
    public List<bool> checkpointsActivos = new List<bool>();

    public int scoreTotal;

    public GameData(
        SaludPersonaje salud,
        AtaquePersonaje ataque,
        PlayerController controller,
        int level,
        string scene,
        int cPoint,
        int slot,
        int score,
        string lastSName)
    {
        slotNumber = slot;

        Vector3 pos = controller.transform.position;
        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;

        vidasJugador = salud.vidasJugador;
        vidasExtras = salud.vidasEXtras;
        balas = ataque.cantidadBalas;
        tipoArma = ataque.seleccionArma;

        currentLevel = level;
        lastScene = scene;
        lastSceneName = lastSName;

        lastCheckPoint = cPoint >= 0 ? cPoint : -1;
        scoreTotal = score;
    }
}
