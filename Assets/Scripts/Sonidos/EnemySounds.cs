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
    [SerializeField] StudioEventEmitter morirMosquitoEmitter;
    [SerializeField] EventReference disparoEnemigo;
    [SerializeField] EventReference recibirDano;
    [SerializeField] EventReference morirSiringuero;

    private EventInstance instanciaRecibirDano;
    private EventInstance instanciaDisparoEnemigo;
    private EventInstance instanciaMorirSiringuero;

    void Start()
    {
        instanciaRecibirDano = RuntimeManager.CreateInstance(recibirDano);
        instanciaDisparoEnemigo = RuntimeManager.CreateInstance(disparoEnemigo);
        instanciaMorirSiringuero = RuntimeManager.CreateInstance(morirSiringuero);
    }
    private void OnEnable()
    {
        SoundEvents.MorirMosquito += ReproducirMorirMosquito;
        SoundEvents.DisparoEnemigo += ReproducirDisparoEnemigo;
        SoundEvents.RecibirDano += ReproducirRecibirDano;
        SoundEvents.MorirSiringuero += ReproducirMorirSiringuero;

    }

    public void ReproducirMorirMosquito()
    {
        if (morirMosquitoEmitter != null)
        {
            morirMosquitoEmitter.Play();
        }
    }

    public void ReproducirDisparoEnemigo(float posicionObjeto)
    {
        if (!disparoEnemigo.IsNull)
        {
            instanciaDisparoEnemigo.start();

            float distancia = Player.transform.position.x - posicionObjeto;
            //Debug.Log("Distancia: " + distancia);

            float distNormalizado = distancia / 8;
            Debug.Log("Distancia Normalizada: " + distNormalizado);
            instanciaDisparoEnemigo.setParameterByName("Panner", -(distNormalizado));
            //disparoEnemigoEmitter.EventInstance.getParameterByName("Panner", out float tipo);
            //disparoEnemigoEmitter.EventInstance.setParameterByName("Tipo", tipo);
            //Debug.Log("Valor actual del emiter: " + tipo);


        }
    }

    public void ReproducirRecibirDano(float posicionObjeto)
    {
        if (!recibirDano.IsNull)
        {
            instanciaRecibirDano.start();

            float distancia = Player.transform.position.x - posicionObjeto;
            //Debug.Log("Distancia: " + distancia);

            float distNormalizado = distancia / 8;
            //Debug.Log("Distancia Normalizada: " + distNormalizado);
            instanciaRecibirDano.setParameterByName("Panner", -(distNormalizado));
            //disparoEnemigoEmitter.EventInstance.setParameterByName("Tipo", tipo);
            //Debug.Log("Valor actual del emiter: " + tipo);


        }
    }
    public void ReproducirMorirSiringuero(float posicionObjeto)
    {
        if (!morirSiringuero.IsNull)
        {           
            instanciaMorirSiringuero.start();
            float distancia = Player.transform.position.x - posicionObjeto;
            float distNormalizado = distancia / 8;
            instanciaMorirSiringuero.setParameterByName("Panner", -(distNormalizado));
        }
    }
}
