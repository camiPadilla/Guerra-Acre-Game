using JetBrains.Annotations;
using PantallaCarga;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    public static MenuPausa instance;
    [SerializeField] private GameObject MenuInGame;
    [SerializeField] List<GameObject> pantallaInGame;
    //[SerializeField] MasterGameManager masterGameManager;
    public LoaderScene loaderScene;

    //Para trabajar con el audio
    [SerializeField] private Scrollbar VolGen;
    [SerializeField] private Scrollbar VolMus;
    [SerializeField] private Scrollbar VolSFX;

    [SerializeField] PlayerSettings playerSettings;
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
        if (loaderScene == null)
        {
            loaderScene = FindObjectOfType<LoaderScene>();
        }
        GetVolumenes();
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
        pantallaInGame[7].SetActive(false);

    }
    public void MostrarPantallaMuerteGame()
    {
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(true);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(false);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
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
    }
    public void MostrarControlesGame()
    {
        //falta agregarla ui de controles
        pantallaInGame[0].SetActive(false);
        pantallaInGame[1].SetActive(false);
        pantallaInGame[2].SetActive(false);
        pantallaInGame[3].SetActive(false);
        pantallaInGame[4].SetActive(true);
        pantallaInGame[5].SetActive(false);
        pantallaInGame[6].SetActive(false);
        pantallaInGame[7].SetActive(false);
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
        pantallaInGame[7].SetActive(true);
    }
    //Setteamos los valores del volumen etc.
    public void GetVolumenes()
    {
        if (PlayerSettings.Instance != null)
        {
            VolGen.onValueChanged.RemoveAllListeners();
            VolMus.onValueChanged.RemoveAllListeners();
            VolSFX.onValueChanged.RemoveAllListeners();

            VolGen.value = PlayerSettings.Instance.GetVolumenGeneral();
            VolMus.value = PlayerSettings.Instance.GetVolumenMusica();
            VolSFX.value = PlayerSettings.Instance.GetVolumenEfectos();

            VolGen.onValueChanged.AddListener(value => PlayerSettings.Instance.SetVolumenGeneral(value));
            VolMus.onValueChanged.AddListener(value => PlayerSettings.Instance.SetVolumenMusica(value));
            VolSFX.onValueChanged.AddListener(value => PlayerSettings.Instance.SetVolumenEfectos(value));
        }
    }
    public void IrMenu()
    {
        loaderScene.LoadSceneString(ConstantsGame.SCENEMAINMENU);
        Destroy(MenuPausa.instance?.gameObject);
        Destroy(HUDManager.instancia?.gameObject);
        Destroy(MenuInGame);
    }
    public void Salir()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

