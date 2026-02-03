using UnityEngine;

public class ControladorVideo : MonoBehaviour
{
    public static ControladorVideo Instance;

    Resolution[] resoluciones;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        AplicarDesdePrefs();
    }

    void AplicarDesdePrefs()
    {
        int calidad = PlayerPrefs.GetInt("numeroDeCalidad", 3);
        QualitySettings.SetQualityLevel(calidad);

        bool full = PlayerPrefs.GetInt("pantallaCompleta", 1) == 1;
        Screen.fullScreen = full;

        resoluciones = Screen.resolutions;
        int res = PlayerPrefs.GetInt("numeroResolucion", resoluciones.Length - 1);
        Resolution r = resoluciones[res];
        Screen.SetResolution(r.width, r.height, full);
    }

    public Resolution[] GetResoluciones()
    {
        return Screen.resolutions;
    }

    public void SetCalidad(int valor)
    {
        QualitySettings.SetQualityLevel(valor);
        PlayerPrefs.SetInt("numeroDeCalidad", valor);
    }

    public void SetPantallaCompleta(bool valor)
    {
        Screen.fullScreen = valor;
        PlayerPrefs.SetInt("pantallaCompleta", valor ? 1 : 0);
    }

    public void SetResolucion(int indice)
    {
        Resolution r = Screen.resolutions[indice];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
        PlayerPrefs.SetInt("numeroResolucion", indice);
    }
}
