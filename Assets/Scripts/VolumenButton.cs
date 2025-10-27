using PantallaCarga;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{

    [SerializeField] private LoaderScene loader;
    [SerializeField] private MasterGameManager master;


    private void Start()
    {
        // Buscar automáticamente el PlayerPrefsManager si no está asignado
        if (LoaderScene.instance == null && MasterGameManager.instance == null)
        {
            loader = FindObjectOfType<LoaderScene>();
            master = FindObjectOfType<MasterGameManager>();

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
