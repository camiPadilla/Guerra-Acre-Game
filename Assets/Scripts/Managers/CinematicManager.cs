using PantallaCarga;
using UnityEngine;
using UnityEngine.Video;

public class CinematicManager : MonoBehaviour
{
    public VideoPlayer vidPlayer;
    public VideoClip[] clips;

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
        vidPlayer.Play();
    }


    void FinVideo(VideoPlayer vp)
    {
        if (!skipped)
            CargarDestino();
    }

    public void Skip()
    {
        if (skipped) return;

        skipped = true;

        vidPlayer.Stop();

        MasterGameManager.instance.CancelarTransiciones();
        CargarDestino();
    }

    void CargarDestino()
    {
        string destino = MasterGameManager.instance.escenaDestinoDespuesDeCinematica;
        MasterGameManager.instance.loaderScene.LoadSceneString(destino);
    }
}