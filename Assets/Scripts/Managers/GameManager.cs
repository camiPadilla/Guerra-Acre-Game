using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] List<GameObject> CheckPoints;

    public static GameManager instancia;
    private int currentDisplay = 0;
    private GameStateSO estadoActual;
    [SerializeField] Camera camaraDisplay1;
    [SerializeField] Camera camaraDisplay2;
    [SerializeField] RCPManager rcMinijuego;
    [SerializeField] GameStateChanger gameStateChanger;
    [SerializeField] List<GameStateSO> estados;
    [SerializeField] GameObject NPCrevivido;
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

    public void CambiarDeEstado(int indice)
    {
        gameStateChanger.SetGameState(estados[indice]);
        SetEstadoActual(estados[indice]);
    }
    void SetEstadoActual(GameStateSO estado)
    {
        estadoActual = estado;
    }
    public void RevivirNPC()
    {
        Debug.Log("lo reviviste");
        NPCrevivido.gameObject.SetActive(true);
        VolverJuego();
    }
    public bool EnDialogo()
    {
        if (estadoActual == estados[4])
        {
            return true;
        }
        else
        {
            return false;
        }
    } 
    public bool Onplaying()
    {
        return estadoActual == estados[0];
    }
    public void CerrarEstado()
    {
        Debug.Log("se cambiara de estado a ");
        switch (estadoActual.stateName)
        {
            case "Paused":
                CambiarDeEstado(0);
                HUDManager.instancia.ReanudarPartida(1);
                
                break;
            case "LeerNota":
                CambiarDeEstado(0);
                HUDManager.instancia.ReanudarPartida(0);
                
                break;
            case "Playing":
                
                HUDManager.instancia.Pausar();
                CambiarDeEstado(1);
                break;
            case "Dialogo":
                CambiarDeEstado (0);
                HUDManager.instancia.ReanudarPartida(9);
                break;
            default:
                break;
        }
    }
}
