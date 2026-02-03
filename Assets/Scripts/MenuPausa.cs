using FMODUnity;
using JetBrains.Annotations;
using PantallaCarga;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] StudioEventEmitter enablePauseSound;
    public static MenuPausa instance;
    [SerializeField] private GameObject MenuInGame;
    [SerializeField] List<GameObject> pantallaInGame;
    [SerializeField] MasterGameManager masterGm;
    void Awake()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(MenuInGame);
        }
        if(masterGm == null)
        {
            masterGm = FindObjectOfType<MasterGameManager>();
        }
    }
    
    public void OcultarTodo()
    {
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
    }
    public void PausarGame()
    {
        pantallaInGame[0].SetActive(true);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);

    }
    public void MostarAjustesGame()
    {
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(true);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
    }
    public void MostrarColeccionablesGame()
    {
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(true);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
    }
    public void MostrarControlesGame()
    {
        //falta agregarla ui de controles
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(true);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
    }
    public void SalirJUego()
    {
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(true);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);

    }
    public void MostrarSonidoGame()
    {
        //falta agregar la ui dsonido
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(true);
        pantallaInGame[6].SetActive(false);

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
        pantallaInGame[6].SetActive(true);
    }
    
    
    public void IrMenuInicio()
    {
        masterGm.IrMenu();
    }
    public void Salir()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

