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
        
    }
}