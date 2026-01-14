using System;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

[Serializable]
public class GameData
{
    public int slotNumber;            

    public float[] position;
    public int vidasJugador;
    public int vidasExtras;
    public int balas;
    public int tipoArma;

    public int currentLevel;
    public string lastScene;
    public string lastSceneName;
    public int lastCheckPoint;
    public List<bool> checkpointsActivos = new List<bool>();

    public int scoreTotal;

    public GameData(SaludPersonaje salud, AtaquePersonaje ataque, PlayerController controller, int level, string scene, int cPoint, int slot, int score, string lastSName)
    {
        slotNumber = slot;

        position = new float[3];
        position[0] = controller.transform.position.x;
        position[1] = controller.transform.position.y;
        position[2] = controller.transform.position.z;

        vidasJugador = salud.vidasJugador;
        vidasExtras = salud.vidasEXtras;
        balas = ataque.cantidadBalas;
        tipoArma = ataque.seleccionArma;

        currentLevel = level;
        lastScene = scene;
        lastSceneName = lastSName;
        lastCheckPoint = cPoint;

        scoreTotal = score; 

        if (checkpointsActivos == null)
            checkpointsActivos = new List<bool>();
    }
}
