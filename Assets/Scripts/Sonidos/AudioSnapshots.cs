using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioSnapshots : MonoBehaviour
{
    [SerializeField] EventInstance snapshotInstance;

    public void ActivarFiltroLectura()
    {
        snapshotInstance = RuntimeManager.CreateInstance("snapshot:/FiltroLectura");
        snapshotInstance.start();
    }

    public void DesactivarFiltroLectura()
    {
        if (snapshotInstance.isValid())
        {
            snapshotInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // deja que haga fade si corresponde
            snapshotInstance.release();
        }
    }
}