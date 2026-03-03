using PantallaCarga;
using UnityEngine;
using UnityEngine.Video;
using FMOD.Studio;
using FMODUnity;
public class CinematicManager : MonoBehaviour
{
    public VideoPlayer vidPlayer;
    public VideoClip[] clips;

    [SerializeField] private EventReference eventoCinematica;
    private EventInstance instanciaCinematica;

    private bool skipped = false;
    private string escenaDestino;

    void Start()
    {
        ConfigurarCinematica();
        vidPlayer.loopPointReached += FinVideo;
    }

    void ConfigurarCinematica()
    {
        int id = MasterGameManager.instance.idCinematicaActual;

        vidPlayer.clip = clips[id];

        // AUDIO
        instanciaCinematica = RuntimeManager.CreateInstance(eventoCinematica);
        instanciaCinematica.setParameterByName("Cinematica", id);
        instanciaCinematica.start();

        vidPlayer.Play();
    }


    void FinVideo(VideoPlayer vp)
    {
        instanciaCinematica.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instanciaCinematica.release();
        if (!skipped)
            CargarDestino();
    }

    public void Skip()
    {
        if (skipped) return;

        skipped = true;

        vidPlayer.Stop();

        instanciaCinematica.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instanciaCinematica.release();

        MasterGameManager.instance.CancelarTransiciones();
        CargarDestino();
    }

    void CargarDestino()
    {
        string destino = MasterGameManager.instance.escenaDestinoDespuesDeCinematica;
        MasterGameManager.instance.loaderScene.LoadSceneString(destino);
    }
}