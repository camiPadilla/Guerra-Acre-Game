using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] List<GameObject> CheckPoints;
    //Este Game Manager estara encargado de los minijuegos y estados.
    [Header("GameManger encargado de minijuegos y estados del juego")]
    public static GameManager instancia;
    private int currentDisplay = 0;
    private GameStateSO estadoActual;
    [SerializeField] Camera camaraDisplay1;
    [SerializeField] Camera camaraDisplay2;
    [SerializeField] RCPManager rcMinijuego;
    [SerializeField] GameStateChanger gameStateChanger;
    [SerializeField] List<GameStateSO> estados;
    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        
        gameStateChanger.GetComponent<GameStateChanger>();
        gameStateChanger.SetGameState(estados[0]);
        SetEstadoActual(estados[0]);
    }
    public void ActivarCamaraDisplay1()
    {
        if (camaraDisplay1 != null && camaraDisplay2 != null)
        {
            camaraDisplay1.gameObject.SetActive(true);
            camaraDisplay2.gameObject.SetActive(false);
            currentDisplay = 0;
        }
    }

    public void ActivarCamaraDisplay2()
    {
        if (camaraDisplay1 != null && camaraDisplay2 != null)
        {
            camaraDisplay1.gameObject.SetActive(false);
            camaraDisplay2.gameObject.SetActive(true);
            currentDisplay = 1;
        }
    }

    // Ejemplo de uso: alternar c�maras
    public void CambiarDeCamara()
    {
        if (currentDisplay == 0)
            ActivarCamaraDisplay2();
        else
            ActivarCamaraDisplay1();
    }
    public void IniciarRPc(GameStateSO estado)
    {
        if (estado.stateName == "RCP")
        {
            CambiarDeCamara();
            rcMinijuego.enabled = true;
        }
    }
    public void VolverJuego()
    {
        gameStateChanger.SetGameState(estados[0]);
        SetEstadoActual(estados[0]);
        CambiarDeCamara();
    }
    public void PausarJuego()
    {
        gameStateChanger.SetGameState(estados[1]);
        SetEstadoActual(estados[1]);
    }
    public void Estadonota()
    {
        gameStateChanger.SetGameState(estados[3]);
        SetEstadoActual(estados[3]);
    }
    void SetEstadoActual(GameStateSO estado)
    {
        estadoActual = estado;
    }
    public void CerrarEstado()
    {
        switch(estadoActual.stateName)
        {
            case "Paused":
                gameStateChanger.SetGameState(estados[0]);
                HUDManager.instancia.ReanudarPartida();
                SetEstadoActual(estados[0]);
                break;
            case "LeerNota":
                gameStateChanger.SetGameState(estados[0]);
                HUDManager.instancia.SalirNota();
                SetEstadoActual(estados[0]);
                break;
            case "Playing":
                PausarJuego();
                HUDManager.instancia.Pausar();
                break;
            default:
                break;
        }
    }
}
