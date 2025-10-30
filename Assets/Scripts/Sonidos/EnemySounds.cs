using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using FMOD.Studio;
using FMODUnity;
using TarodevController;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] PlayerController Player;
    [SerializeField] EventReference morirMosquitoEmitter;
    [SerializeField] StudioEventEmitter vueloMosquito;
    [SerializeField] EventReference disparoEnemigo;
    [SerializeField] EventReference recibirDano;
    [SerializeField] EventReference morirSiringuero;

    private EventInstance instanciaRecibirDano;
    private EventInstance instanciaDisparoEnemigo;
    private EventInstance instanciaMorirSiringuero;
    private EventInstance instanciaMorirMosquito;

    // --- Añadido ---
    private bool mosquitoVivo = true;
    private bool vueloActivo = true;
    // ----------------

    void Start()
    {
        instanciaRecibirDano = RuntimeManager.CreateInstance(recibirDano);
        instanciaDisparoEnemigo = RuntimeManager.CreateInstance(disparoEnemigo);
        instanciaMorirSiringuero = RuntimeManager.CreateInstance(morirSiringuero);
        instanciaMorirMosquito = RuntimeManager.CreateInstance(morirMosquitoEmitter);

        // --- Añadido ---
        /*if (vueloMosquito != null && !vueloActivo && Player != null)
        {
            vueloMosquito.Play();
            vueloActivo = true;
        }*/
        // ---------------
    }

    private void OnEnable()
    {
        SoundEvents.MorirMosquito += ReproducirMorirMosquito;
        SoundEvents.DisparoEnemigo += ReproducirDisparoEnemigo;
        SoundEvents.RecibirDano += ReproducirRecibirDano;
        SoundEvents.MorirSiringuero += ReproducirMorirSiringuero;
    }

    // --- Añadido ---
    private void OnDisable()
    {
        SoundEvents.MorirMosquito -= ReproducirMorirMosquito;
        SoundEvents.DisparoEnemigo -= ReproducirDisparoEnemigo;
        SoundEvents.RecibirDano -= ReproducirRecibirDano;
        SoundEvents.MorirSiringuero -= ReproducirMorirSiringuero;
    }
    // ---------------

    public void ReproducirMorirMosquito()
    {
        if (!morirMosquitoEmitter.IsNull && Player != null)
        {
            instanciaMorirMosquito.start();
        }

        // --- Añadido ---
        mosquitoVivo = false;
        if (vueloMosquito != null && vueloActivo)
        {
            vueloMosquito.Stop();
            vueloActivo = false;
        }
        // ---------------
    }

    public void ReproducirDisparoEnemigo(float posicionObjeto)
    {
        if (!disparoEnemigo.IsNull && Player != null)
        {
            instanciaDisparoEnemigo.start();

            float distancia = Player.transform.position.x - posicionObjeto;
            float distNormalizado = distancia / 8;
            Debug.Log("Distancia Normalizada: " + distNormalizado);
            instanciaDisparoEnemigo.setParameterByName("Panner", -(distNormalizado));
        }
    }

    public void ReproducirRecibirDano(float posicionObjeto)
    {
        if (!recibirDano.IsNull && Player != null)
        {
            instanciaRecibirDano.start();

            float distancia = Player.transform.position.x - posicionObjeto;
            float distNormalizado = distancia / 8;
            instanciaRecibirDano.setParameterByName("Panner", -(distNormalizado));
        }
    }

    public void ReproducirMorirSiringuero(float posicionObjeto)
    {
        if (!morirSiringuero.IsNull && Player != null)
        {
            instanciaMorirSiringuero.start();
            float distancia = Player.transform.position.x - posicionObjeto;
            float distNormalizado = distancia / 8;
            instanciaMorirSiringuero.setParameterByName("Panner", -(distNormalizado));
        }
    }

    /*public void ReproducirVueloMosquito(float posicionObjeto)
    {
        if (vueloMosquito != null && Player != null && mosquitoVivo)
        {
            if (!vueloActivo)
            {
                vueloMosquito.Play();
                vueloActivo = true;
            }

            float distancia = Player.transform.position.x - posicionObjeto;
            float distNormalizado = Mathf.Clamp(distancia / 8, -1f, 1f);
            vueloMosquito.EventInstance.setParameterByName("Panner", -(distNormalizado));
        }
    }*/

    // --- Añadido ---
    private void Update()
    {
        // Actualiza el Panner constantemente mientras el mosquito está vivo y el sonido activo
        if (vueloMosquito != null && vueloActivo && Player != null && mosquitoVivo)
        {
            float distancia = Player.transform.position.x - transform.position.x;
            float distNormalizado = Mathf.Clamp(distancia / 8, -1f, 1f);
            vueloMosquito.EventInstance.setParameterByName("Panner", -(distNormalizado));
        }
    }
}
