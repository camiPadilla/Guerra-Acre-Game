using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] List<GameObject> CheckPoints;
    
    public static GameManager instancia;
    private int currentDisplay = 0;
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
        CambiarDeCamara();
    }
}
