using PantallaCarga;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    [SerializeField] private PlayerSettings prefsSet;
    [SerializeField] private LoaderScene loader;
    [SerializeField] private MasterGameManager master;


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
    public void IrMenu()
    {
        loader.LoadSceneString("MainMenu");
    }
    public void ReintentarNivel()
    {
        if (loader != null && master != null)
        {
            loader.LoadSceneString(master.escenaActual);
            print("Reintentando nivel: " + master.escenaActual);
        }
    }
    public void SigNivel()
    {
        if (loader != null && master != null)
        {
            loader.LoadSceneString(master.escenaSiguiente);
            print("Cargando siguiente nivel: " + master.escenaSiguiente);
        }
    }
    public void Guardar(){
        if (master != null)
        {
            master.SaveGame();
        }
    }

}
