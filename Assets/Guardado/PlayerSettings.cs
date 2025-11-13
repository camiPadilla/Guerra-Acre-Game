using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings Instance { get; private set; }

    [Header("Sliders del Menú Principal")]
    [SerializeField] private Scrollbar VolumenGeneral;
    [SerializeField] private Scrollbar VolumenMusica;
    [SerializeField] private Scrollbar VolumenEfectos;

    [Header("Sliders del Menú de Pausa")]
    [SerializeField] private Scrollbar VolGenPause;
    [SerializeField] private Scrollbar VolMusPause;
    [SerializeField] private Scrollbar VolEfcPause;

    private bool initialized = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        LoadSettings();
        InitializeListeners();
        initialized = true;
        SyncUI();
    }

    private void InitializeListeners()
    {
        // Menú principal
        if (VolumenGeneral) VolumenGeneral.onValueChanged.AddListener(SetVolumenGeneral);
        if (VolumenMusica) VolumenMusica.onValueChanged.AddListener(SetVolumenMusica);
        if (VolumenEfectos) VolumenEfectos.onValueChanged.AddListener(SetVolumenEfectos);

        // Menú pausa
        if (VolGenPause) VolGenPause.onValueChanged.AddListener(SetVolumenGeneral);
        if (VolMusPause) VolMusPause.onValueChanged.AddListener(SetVolumenMusica);
        if (VolEfcPause) VolEfcPause.onValueChanged.AddListener(SetVolumenEfectos);
    }

    public void LoadSettings()
    {
        float volGeneral = PlayerPrefs.GetFloat("VolumenGeneral", 1f);
        float volMusica = PlayerPrefs.GetFloat("VolumenMusica", 1f);
        float volEfectos = PlayerPrefs.GetFloat("VolumenEfectos", 1f);

        if (VolumenGeneral) VolumenGeneral.value = volGeneral;
        if (VolumenMusica) VolumenMusica.value = volMusica;
        if (VolumenEfectos) VolumenEfectos.value = volEfectos;

        if (VolGenPause) VolGenPause.value = volGeneral;
        if (VolMusPause) VolMusPause.value = volMusica;
        if (VolEfcPause) VolEfcPause.value = volEfectos;
    }

    private void SyncUI()
    {
        if (!initialized) return;

        if (VolGenPause && VolumenGeneral)
            VolGenPause.value = VolumenGeneral.value;

        if (VolMusPause && VolumenMusica)
            VolMusPause.value = VolumenMusica.value;

        if (VolEfcPause && VolumenEfectos)
            VolEfcPause.value = VolumenEfectos.value;
    }

    public void SetVolumenGeneral(float value)
    {
        PlayerPrefs.SetFloat("VolumenGeneral", value);
        PlayerPrefs.Save();
        SyncUI();
    }

    public void SetVolumenMusica(float value)
    {
        PlayerPrefs.SetFloat("VolumenMusica", value);
        PlayerPrefs.Save();
        SyncUI();
    }

    public void SetVolumenEfectos(float value)
    {
        PlayerPrefs.SetFloat("VolumenEfectos", value);
        PlayerPrefs.Save();
        SyncUI();
    }

    public void ResetDefaultValues()
    {
        PlayerPrefs.DeleteAll();
        LoadSettings();
    }

    public float GetVolumenGeneral() => PlayerPrefs.GetFloat("VolumenGeneral", 1f);
    public float GetVolumenMusica() => PlayerPrefs.GetFloat("VolumenMusica", 1f);
    public float GetVolumenEfectos() => PlayerPrefs.GetFloat("VolumenEfectos", 1f);
}