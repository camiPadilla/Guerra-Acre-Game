using UnityEngine;

public class GameStateChanger : MonoBehaviour
{
    public GameManagerSO gameManager;

    public void SetGameState(GameStateSO estadoJuego)
    {
            gameManager.CambiarEstado(estadoJuego);
    }
    public void RestorePreviousState()
    {
            gameManager.RestaurarEstado();
    }

}
