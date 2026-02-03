using FMODUnity;
using JetBrains.Annotations;
using PantallaCarga;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] StudioEventEmitter enablePauseSound;
    public static MenuPausa instance;
    [SerializeField] private GameObject MenuInGame;
    [SerializeField] List<GameObject> pantallaInGame;
    [SerializeField] MasterGameManager masterGm;
    //para los aujstes de video
    public TMP_Dropdown dropCalidad;
    public TMP_Dropdown dropResoluciones;
    public Toggle pantCompleta;
    void OnEnable()
    {
        StartCoroutine(InitVideoSeguro());
    }
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

    IEnumerator InitVideoSeguro()
    {
        yield return null;

        if (ControladorVideo.Instance == null)
        {
            Debug.LogError("ControladorVideo no existe");
            yield break;
        }

        InicializarVideoUI();
        ConectarEventosVideo();
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
    void InicializarVideoUI()
    {
        // CALIDAD
        dropCalidad.SetValueWithoutNotify(
            PlayerPrefs.GetInt("numeroDeCalidad", QualitySettings.GetQualityLevel())
        );

        // PANTALLA COMPLETA
        pantCompleta.SetIsOnWithoutNotify(
            PlayerPrefs.GetInt("pantallaCompleta", 1) == 1
        );

        // RESOLUCIONES
        Resolution[] res = ControladorVideo.Instance.GetResoluciones();
        dropResoluciones.ClearOptions();

        List<string> opciones = new List<string>();
        for (int i = 0; i < res.Length; i++)
            opciones.Add($"{res[i].width} x {res[i].height}");

        dropResoluciones.AddOptions(opciones);

        int resActual = PlayerPrefs.GetInt("numeroResolucion", res.Length - 1);
        dropResoluciones.SetValueWithoutNotify(resActual);

        dropResoluciones.RefreshShownValue();
        dropCalidad.RefreshShownValue();
    }
    void ConectarEventosVideo()
    {
        dropCalidad.onValueChanged.RemoveAllListeners();
        dropResoluciones.onValueChanged.RemoveAllListeners();
        pantCompleta.onValueChanged.RemoveAllListeners();

        dropCalidad.onValueChanged.AddListener(OnCambiarCalidad);
        dropResoluciones.onValueChanged.AddListener(OnCambiarResolucion);
        pantCompleta.onValueChanged.AddListener(OnPantallaCompleta);
    }


    public void OnCambiarCalidad(int v)
    {
        ControladorVideo.Instance.SetCalidad(v);
    }

    public void OnCambiarResolucion(int v)
    {
        ControladorVideo.Instance.SetResolucion(v);
    }

    public void OnPantallaCompleta(bool v)
    {
        ControladorVideo.Instance.SetPantallaCompleta(v);
    }
}

