using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using FMOD.Studio;
using FMODUnity;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] PlayerController Player;
    [SerializeField] MosquitoController mosquito;

    [SerializeField] EventReference morirMosquitoEmitter;
    [SerializeField] EventReference disparoEnemigo;
    [SerializeField] EventReference recibirDano;
    [SerializeField] EventReference morirSiringuero;

    private EventInstance instanciaRecibirDano;
    private EventInstance instanciaDisparoEnemigo;
    private EventInstance instanciaMorirSiringuero;
    private EventInstance instanciaMorirMosquito;

    void Start()
    {
        instanciaRecibirDano = RuntimeManager.CreateInstance(recibirDano);
        instanciaDisparoEnemigo = RuntimeManager.CreateInstance(disparoEnemigo);
        instanciaMorirSiringuero = RuntimeManager.CreateInstance(morirSiringuero);
        instanciaMorirMosquito = RuntimeManager.CreateInstance(morirMosquitoEmitter);

    }

    private void OnEnable()
    {
        SoundEvents.MorirMosquito += ReproducirMorirMosquito;
        SoundEvents.DisparoEnemigo += ReproducirDisparoEnemigo;
        SoundEvents.RecibirDano += ReproducirRecibirDano;
        SoundEvents.MorirSiringuero += ReproducirMorirSiringuero;
    }

    private void OnDisable()
    {
        SoundEvents.MorirMosquito -= ReproducirMorirMosquito;
        SoundEvents.DisparoEnemigo -= ReproducirDisparoEnemigo;
        SoundEvents.RecibirDano -= ReproducirRecibirDano;
        SoundEvents.MorirSiringuero -= ReproducirMorirSiringuero;
    }
    public void ReproducirMorirMosquito(float posicionObjeto)
    {
        if (!morirMosquitoEmitter.IsNull && Player != null)
        {
            instanciaMorirMosquito.start();
            float distancia = Player.transform.position.x - posicionObjeto;
            float distNormalizado = distancia / 8;
            Debug.Log("Distancia Normalizada: " + distNormalizado);
            instanciaMorirMosquito.setParameterByName("Panner", -(distNormalizado));
        }
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
}
