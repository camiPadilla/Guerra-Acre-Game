using UnityEngine.UI;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings Instance { get;  set; }
    //Agregar las cosas que hacen falta para guardar los datos, osea sonido , etc.
    [SerializeField] private Scrollbar VolumenGeneral;
    [SerializeField] private Scrollbar VolumenMusica;
    [SerializeField] private Scrollbar VolumenEfectos;
    [SerializeField] private Scrollbar VolGenPause;
    [SerializeField] private Scrollbar VolMusPause;
    [SerializeField] private Scrollbar VolEfcPause;
    //Agregar resolucion y tamaño de pantalla, supongo que aplicaremos 
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        LoadSettings();

        VolumenGeneral.onValueChanged.AddListener(SetVolumenGeneral);
        VolumenMusica.onValueChanged.AddListener(SetVolumenMusica);
        VolumenEfectos.onValueChanged.AddListener(SetVolumenEfectos);
        VolGenPause.onValueChanged.AddListener(SetVolumenGeneral);
        VolMusPause.onValueChanged.AddListener(SetVolumenMusica);
        VolEfcPause.onValueChanged.AddListener(SetVolumenEfectos);
    }

    public void LoadSettings()
    {
        VolGenPause.value = PlayerPrefs.GetFloat("VolumenGeneral");
        VolMusPause.value = PlayerPrefs.GetFloat("VolumenMusica");
        VolEfcPause.value = PlayerPrefs.GetFloat("VolumenEfectos");
    }
    public void SetVolumenGeneral(float value)
    {
        PlayerPrefs.SetFloat("VolumenGeneral", value);
    }

    public void SetVolumenMusica(float value)
    {
        PlayerPrefs.SetFloat("VolumenMusica", value);
    }

    public void SetVolumenEfectos(float value)
    {
        PlayerPrefs.SetFloat("VolumenEfectos", value);
    }
    public void ReserDefaultValues()
    {
        PlayerPrefs.DeleteAll();
    }
    public float GetVolumenGeneral()
    {
        return VolumenGeneral.value;
    } 
    public float GetVolumenMusica()
    {
        return VolumenMusica.value;
    } 
    public float GetVolumenEfectos()
    {
        return VolumenEfectos.value;
    }
}