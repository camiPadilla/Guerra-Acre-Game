using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] List<GameObject> pantallasMainMenu;
    [SerializeField] MasterGameManager masterGameManager;


    [SerializeField] string escenaPartida;
    void Awake()
    {
            // DontDestroyOnLoad(MainMenu);
            foreach(SlotButton slot in FindObjectsOfType<SlotButton>())
            {
                slot.ActualizarVisual();
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        IrMenu();
    }

    // Update is called once per frame
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
        pantallasMainMenu[8].SetActive(false);
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
        pantallasMainMenu[8].SetActive(false);
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
        pantallasMainMenu[8].SetActive(false);
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
        pantallasMainMenu[8].SetActive(false);
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
        pantallasMainMenu[8].SetActive(false);
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
        pantallasMainMenu[8].SetActive(false);
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
        pantallasMainMenu[8].SetActive(false);
    }
    public void Desactivado()
    {
        foreach (var pantalla in pantallasMainMenu)
        {
           pantalla.SetActive(false);
        }
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
   
}
