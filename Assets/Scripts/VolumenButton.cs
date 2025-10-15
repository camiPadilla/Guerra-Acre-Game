using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    private PlayerSettings prefsSet;

    private void Awake()
    {
        // Buscar automáticamente el PlayerPrefsManager si no está asignado
        if (PlayerSettings.Instance != null)
        {
            prefsSet = PlayerSettings.Instance;
        }
        else
        {
            prefsSet = FindObjectOfType<PlayerSettings>();
        }
    }

    public void CambiarVolumenGen()
    {
        if (prefsSet != null)
        {
            prefsSet.SetVolumenGeneral();
        }
    }
    public void CambiarVolumenMus()
    {
        if (prefsSet != null)
        {
            prefsSet.SetVolumenMusica();
        }
    }
    public void CambiarVolumenEfe()
    {
        if (prefsSet != null)
        {
            prefsSet.SetVolumenEfectos();
        }
    }
}
