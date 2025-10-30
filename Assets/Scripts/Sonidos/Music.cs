using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    [SerializeField] EventReference musicNivel_1;
    [SerializeField] EventReference musicNivel_2;

    private EventInstance instanciaNivel1;
    private EventInstance instanciaNivel2;

    private void OnEnable()
    {
        SoundEvents.DetenerMusica += DetenerMusica;
    }
    void Start()
    {

        instanciaNivel1 = RuntimeManager.CreateInstance(musicNivel_1);
        instanciaNivel2 = RuntimeManager.CreateInstance(musicNivel_2);

        Scene escenaActiva = SceneManager.GetActiveScene();

        if (escenaActiva.name == "EscenaUno")
        {
            instanciaNivel1.start();
        }
        else if (escenaActiva.name == "EscenaDos")
        {
            instanciaNivel2.start();
            UnityEngine.Debug.Log("Reproduciendo musica nivel 2");
        }
        Debug.Log("La escena activa es: " + escenaActiva.name);
    }
    public void DetenerMusica()
    {
        instanciaNivel1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instanciaNivel2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
