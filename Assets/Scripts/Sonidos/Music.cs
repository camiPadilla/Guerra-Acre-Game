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
    [SerializeField] EventReference musicVictory;

    private EventInstance instanciaNivel1;
    private EventInstance instanciaNivel2;
    private EventInstance instanciaVictory;

    static Music instancia;

    void Awake()
    {
        if (instancia != null)
        {
            Destroy(gameObject);
            return;
        }
        instancia = this;
    }

    private void OnEnable()
    {
        SoundEvents.DetenerMusica += DetenerMusica;
        SoundEvents.PlayVictory += PlayVictory;
    }

    void OnDestroy()
    {
        instanciaNivel1.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instanciaNivel1.release();

        instanciaNivel2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instanciaNivel2.release();

        instanciaVictory.release();
    }

    private void OnDisable()
    {
        SoundEvents.DetenerMusica -= DetenerMusica;
        SoundEvents.PlayVictory -= PlayVictory;
    }

    void Start()
    {
        instanciaNivel1 = RuntimeManager.CreateInstance(musicNivel_1);
        instanciaNivel2 = RuntimeManager.CreateInstance(musicNivel_2);
        instanciaVictory = RuntimeManager.CreateInstance(musicVictory);

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
    public void PlayVictory()
    {
        if (!musicVictory.IsNull)
            RuntimeManager.PlayOneShot(musicVictory);
    }
}
