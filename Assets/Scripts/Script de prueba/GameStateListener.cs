using ScriptableObjectArchitecture;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class GameStateListener : MonoBehaviour
{
    public GameStateSOGameEvent gameStateChanged;
    public MonoBehaviour[] componentes;
    public List<GameStateSO> estadosActivados;
    public List<GameStateSO> estadosDesactivados;

    [Header("Acciones")]
    public UnityEvent onPlayingState;
    public UnityEvent onPausedState;
    // Start is called before the first frame update
    private void OnEnable()
    {
        gameStateChanged.AddListener(GameStatedChanged);

    }
    private void OnDisable()
    {
        gameStateChanged.RemoveListener(GameStatedChanged);
    }

    private void GameStatedChanged(GameStateSO nuevoEstadoJuego)
    {
        InvokeShortCuts(nuevoEstadoJuego);
        InvokeAction(nuevoEstadoJuego);
    }
    private void InvokeShortCuts(GameStateSO estadoJuego)
    {
        foreach (var component in componentes)
        {
            if (estadosActivados.Contains(estadoJuego))
            {


                component.enabled = true;
            }
            if (estadosDesactivados.Contains(estadoJuego))
            {
                component.enabled = false;
            }

        }      

    }
    private void InvokeAction(GameStateSO NuevoEstadoJuego)
    {
        if(NuevoEstadoJuego.stateName == "Playing" && onPlayingState !=null)
        {
            onPlayingState.Invoke();
        }
        if(NuevoEstadoJuego.stateName == "Paused" && onPausedState != null)
        {
            onPausedState.Invoke();
        }
    }
}
