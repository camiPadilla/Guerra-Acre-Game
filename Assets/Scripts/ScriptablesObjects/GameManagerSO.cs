using ScriptableObjectArchitecture;
using UnityEngine;
[CreateAssetMenu(fileName = "GameManager", menuName= "SO/Game Manager")]
public class GameManagerSO : ScriptableObject
{
    public GameStateSO estadoActual;

    [Header("eventos")]

    private GameStateSO estadoPrevio;
    

    public GameStateSOGameEvent gameStateChanged;
    public void CambiarEstado(GameStateSO nuevoEstado)
    {
        if (nuevoEstado != null)
            estadoPrevio = estadoActual;
        estadoActual = nuevoEstado;
        if(gameStateChanged != null)
            gameStateChanged.Raise(estadoActual);

    }
    public void RestaurarEstado()
    {
        CambiarEstado(estadoPrevio);
    }
}