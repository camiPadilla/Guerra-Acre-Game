using UnityEngine;

public class GameStateChanger : MonoBehaviour
{
    public GameManagerSO gameManager;

    public void SetGameState(GameStateSO estadoJuego)
        {
        Debug.Log("Cambiando estado de juego a: " + estadoJuego.stateName);
            gameManager.CambiarEstado(estadoJuego);
    }
    public void RestorePreviousState()
    {
            gameManager.RestaurarEstado();
    }

}
