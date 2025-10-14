using System.Collections.Generic;
using TarodevController;

[System.Serializable]

public class GameData
{
    
}
public class PlayerData
{
    public int vidasJugador;
    public int vidasEXtras;
    public float[] position = new float[3];
    //para guardar cantidad de balas, y si la esta usando
    public int balas;
    public int tipoArma;
    public PlayerData(SaludPersonaje playerSalud, AtaquePersonaje PlayerAtaque, PlayerController player)
    {
        vidasJugador = playerSalud.vidasJugador;
        vidasEXtras = playerSalud.vidasEXtras;
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        //constructor para las balas, etc
        balas = PlayerAtaque.GetBalasActuales();
        tipoArma = PlayerAtaque.seleccionArma;
    }
    public class ProgressData
    {
        public int currentLevel;
        public List<int> checkPointsActivos;
        public ProgressData(int nivelActual, List<int> cpActivos)
        {
            this.currentLevel = nivelActual;
            this.checkPointsActivos = cpActivos;
        }

    }
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
    //quiero creer que para las notas, en el area de coleccionables, hacer una lista, y guardar el index de las notas que se han recogido, supongo que solo eso
    //si se me ocurre algo mas, ya veremos mmmmhh
}