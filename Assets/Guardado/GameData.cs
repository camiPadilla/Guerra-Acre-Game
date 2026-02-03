using System;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

[Serializable]
public class GameData
{
    // Player stats
    public int vidasJugador;
    public int vidasExtras;
    public int balas;
    public int tipoArma;

    // Progreso
    public int currentLevel;
    public string lastScene;
    public int slot;
    public int scoreTotal;
    public string lastSceneName;

    // Posición
    public float[] position;

    // Escena
    public List<int> cajasDestruidas;
    public List<int> enemigosMuertos;
    public List<int> objetosRecogidos;

    public GameData(
        SaludPersonaje salud,
        AtaquePersonaje ataque,
        PlayerController controller,
        int level,
        string scene,
        int slot,
        int score,
        string sceneName
    )
    {
        vidasJugador = salud.vidasJugador;
        vidasExtras = salud.vidasEXtras;
        balas = ataque.cantidadBalas;
        tipoArma = ataque.seleccionArma;

        currentLevel = level;
        lastScene = scene;
        this.slot = slot;
        scoreTotal = score;
        lastSceneName = sceneName;

        position = new float[2];
        position[0] = controller.transform.position.x;
        position[1] = controller.transform.position.y;

        cajasDestruidas = new List<int>();
        enemigosMuertos = new List<int>();
        objetosRecogidos = new List<int>();
    }
}
