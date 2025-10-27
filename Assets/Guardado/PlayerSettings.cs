using UnityEngine.UI;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings Instance { get;  set; }
    //Agregar las cosas que hacen falta para guardar los datos, osea sonido , etc.
    [SerializeField] private Scrollbar VolumenGeneral;
    [SerializeField] private Scrollbar VolumenMusica;
    [SerializeField] private Scrollbar VolumenEfectos;
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
    }

    public void LoadSettings()
    {
        VolumenGeneral.value = PlayerPrefs.GetFloat("VolumenGeneral");
        VolumenMusica.value = PlayerPrefs.GetFloat("VolumenMusica");
        VolumenEfectos.value = PlayerPrefs.GetFloat("VolumenEfectos");
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