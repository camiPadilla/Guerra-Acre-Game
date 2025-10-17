using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public static UIManager instancia;
    [SerializeField] private GameObject Menues;
    [SerializeField] List<GameObject> pantallasMainMenu;
    [SerializeField] List<GameObject> pantallaInGame;
    [SerializeField] MasterGameManager masterGameManager;


    [SerializeField] string escenaPartida;
    void Awake()
    {
        if(instancia != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(Menues);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        IrMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IrMenu()
    {
        pantallasMainMenu[0].SetActive(true);
        pantallasMainMenu[1].SetActive(false);
        pantallasMainMenu[2].SetActive(false);
        pantallasMainMenu[3].SetActive(false);
        pantallasMainMenu[4].SetActive(false);
        pantallasMainMenu[5].SetActive(false);
       pantallasMainMenu[6].SetActive(false);
        pantallasMainMenu[7].SetActive(false);
    }
    public void IrColeccionables()
    {
        pantallasMainMenu[0].SetActive(false);
        pantallasMainMenu[1].SetActive(true);
        pantallasMainMenu[2].SetActive(false);
        pantallasMainMenu[3].SetActive(false);
        pantallasMainMenu[4].SetActive(false);
       pantallasMainMenu[5].SetActive(false);
        pantallasMainMenu[6].SetActive(false);
        pantallasMainMenu[7].SetActive(false);
    }
    public void IrMenuPartida()
    {
         pantallasMainMenu[0].SetActive(false);
        pantallasMainMenu[1].SetActive(false);
        pantallasMainMenu[2].SetActive(true);
        pantallasMainMenu[3].SetActive(false);
        pantallasMainMenu[4].SetActive(false);
        pantallasMainMenu[5].SetActive(false);
        pantallasMainMenu[6].SetActive(false);
        pantallasMainMenu[7].SetActive(false);
        pantallasMainMenu[8].SetActive(false);
    }
    public void IrSalir()
    {
         pantallasMainMenu[0].SetActive(false);
        pantallasMainMenu[1].SetActive(false);
        pantallasMainMenu[2].SetActive(false);
        pantallasMainMenu[3].SetActive(true);
        pantallasMainMenu[4].SetActive(false);
        pantallasMainMenu[5].SetActive(false);
       pantallasMainMenu[6].SetActive(false);
        pantallasMainMenu[7].SetActive(false);
    }
    public void IrOpciones()
    {
        pantallasMainMenu[0].SetActive(false);
        pantallasMainMenu[1].SetActive(false);
        pantallasMainMenu[2].SetActive(false);
        pantallasMainMenu[3].SetActive(false);
        pantallasMainMenu[4].SetActive(true);
        pantallasMainMenu[5].SetActive(false);
        pantallasMainMenu[6].SetActive(false);
        pantallasMainMenu[7].SetActive(false);
    }
    public void IrAudio()
    {
        pantallasMainMenu[0].SetActive(false);
        pantallasMainMenu[1].SetActive(false);
        pantallasMainMenu[2].SetActive(false);
        pantallasMainMenu[3].SetActive(false);
        pantallasMainMenu[4].SetActive(false);
        pantallasMainMenu[5].SetActive(true);
        pantallasMainMenu[6].SetActive(false);
        pantallasMainMenu[7].SetActive(false);
    }
    public void IrControles()
    {
        pantallasMainMenu[0].SetActive(false);
        pantallasMainMenu[1].SetActive(false);
        pantallasMainMenu[2].SetActive(false);
        pantallasMainMenu[3].SetActive(false);
        pantallasMainMenu[4].SetActive(false);
        pantallasMainMenu[5].SetActive(false);
        pantallasMainMenu[6].SetActive(true);
        pantallasMainMenu[7].SetActive(false);
    }
    public void IrVideo()
    {
         pantallasMainMenu[0].SetActive(false);
        pantallasMainMenu[1].SetActive(false);
        pantallasMainMenu[2].SetActive(false);
        pantallasMainMenu[3].SetActive(false);
        pantallasMainMenu[4].SetActive(false);
        pantallasMainMenu[5].SetActive(false);
        pantallasMainMenu[6].SetActive(false);
        pantallasMainMenu[7].SetActive(true);
    }
    public void Desactivado()
    {
        pantallasMainMenu[0].SetActive(false);
        pantallasMainMenu[1].SetActive(false);
        pantallasMainMenu[2].SetActive(false);
        pantallasMainMenu[3].SetActive(false);
        pantallasMainMenu[4].SetActive(false);
        pantallasMainMenu[5].SetActive(false);
        pantallasMainMenu[6].SetActive(false);
        pantallasMainMenu[7].SetActive(false);
    }
    //Si tiene partidas guardadas que vaya a este menu, aun por ver....
    public void IrCargar()
    {
        pantallasMainMenu[0].SetActive(false);
        pantallasMainMenu[1].SetActive(false);
        pantallasMainMenu[2].SetActive(false);
        pantallasMainMenu[3].SetActive(false);
        pantallasMainMenu[4].SetActive(false);
        pantallasMainMenu[5].SetActive(false);
        pantallasMainMenu[6].SetActive(false);
        pantallasMainMenu[7].SetActive(false);
        pantallasMainMenu[8].SetActive(true);
    }
    public void Salir()
    {
        Application.Quit();
    }
    public void Comezar()
    {
        SceneManager.LoadScene(escenaPartida);
    }
    //Menus InGame
    public void PausarGame()
    {
        pantallaInGame[0].SetActive(true);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
        pantallaInGame[8].SetActive(false);

    }
    public void MostrarPantallaMuerteGame()
    {
        pantallaInGame[1].SetActive(true);
        pantallaInGame[0].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
        pantallaInGame[8].SetActive(false);
    }
    public void MostrarPantallaFinGame()
    {
        pantallaInGame[4].SetActive(true);
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
        pantallaInGame[8].SetActive(false);
    }
    public void OcultarTodoGame()
    {
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
        pantallaInGame[8].SetActive(false);
        Time.timeScale = 1;
    }
    public void MostarAjustesGame()
    {
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(true);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
        pantallaInGame[8].SetActive(false);

    }
    public void MostrarColeccionablesGame()
    {
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(true);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
        pantallaInGame[8].SetActive(false);
    }
    public void MostrarControlesGame()
    {
        //falta agregarla ui de controles
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(true);
        pantallaInGame[8].SetActive(false);
    }
    public void MostrarSonidoGame()
    {
        //falta agregar la ui dsonido
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(true);
        pantallaInGame[7].SetActive(false);
        pantallaInGame[8].SetActive(false);
    }
    public void MostrarVideoGame()
    {
        //falta agregar la ui de video
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
        pantallaInGame[8].SetActive(true);
    }
    public void SalirJUego()
    {
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(true);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
        pantallaInGame[8].SetActive(false);

    }
    public void IrInicio()
    {
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
    }
}
