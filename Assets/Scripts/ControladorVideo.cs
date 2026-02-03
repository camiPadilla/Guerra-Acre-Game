using UnityEngine;

public class ControladorVideo : MonoBehaviour
{
    public static ControladorVideo Instance;

    Resolution[] resoluciones;

    void Awake()
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
        resoluciones = Screen.resolutions;
        AplicarDesdePrefs();
    }

    void AplicarDesdePrefs()
    {
        // ===== CALIDAD =====
        int calidad = PlayerPrefs.GetInt("numeroDeCalidad", QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(calidad, true);

        // ===== PANTALLA COMPLETA =====
        bool full = PlayerPrefs.GetInt("pantallaCompleta", 1) == 1;
        Screen.fullScreenMode = full
            ? FullScreenMode.FullScreenWindow
            : FullScreenMode.Windowed;

        // ===== RESOLUCIÓN =====
        int resIndex = PlayerPrefs.GetInt("numeroResolucion", resoluciones.Length - 1);
        Resolution r = resoluciones[resIndex];

        Screen.SetResolution(r.width, r.height, Screen.fullScreenMode);
    }

    // ================== UI ==================

    public void SetCalidad(int valor)
    {
        PlayerPrefs.SetInt("numeroDeCalidad", valor);
        PlayerPrefs.Save();

        QualitySettings.SetQualityLevel(valor, true);
    }

    public void SetPantallaCompleta(bool valor)
    {
        PlayerPrefs.SetInt("pantallaCompleta", valor ? 1 : 0);
        PlayerPrefs.Save();

        Screen.fullScreen = valor;
    }

    public void SetResolucion(int indice)
    {
        PlayerPrefs.SetInt("numeroResolucion", indice);
        PlayerPrefs.Save();

        AplicarResolucionActual();
    }

    void AplicarResolucionActual()
    {
        int indice = PlayerPrefs.GetInt("numeroResolucion", resoluciones.Length - 1);
        Resolution r = resoluciones[indice];

        Screen.SetResolution(
            r.width,
            r.height,
            Screen.fullScreenMode
        );
    }

    public Resolution[] GetResoluciones()
    {
        return resoluciones;
    }
}
