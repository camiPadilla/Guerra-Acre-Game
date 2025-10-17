using UnityEngine.UI;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings Instance { get; private set; }
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
    }
    private void LoadSettings()
    {
        VolumenGeneral.value = PlayerPrefs.GetFloat("VolumenGeneral");
        VolumenMusica.value = PlayerPrefs.GetFloat("VolumenMusica");
        VolumenEfectos.value = PlayerPrefs.GetFloat("VolumenEfectos");
    }
    public void SetVolumenGeneral()
    {
        PlayerPrefs.SetFloat("VolumenGeneral", VolumenGeneral.value);
    }
    public void SetVolumenMusica()
    {
        PlayerPrefs.SetFloat("VolumenMusica", VolumenMusica.value);
    }
    public void SetVolumenEfectos()
    {
        PlayerPrefs.SetFloat("VolumenEfectos", VolumenEfectos.value);
    }
    public void ReserDefaultValues()
    {
        PlayerPrefs.DeleteAll();
    }
}