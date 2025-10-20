using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;
using UnityEngine.UIElements;

public class EnvironmentSounds : MonoBehaviour
{
    [SerializeField] AtaquePersonaje ataquePersonaje;

    [SerializeField] Proyectil piedra;

    [SerializeField] StudioEventEmitter destruirObjetoEmitter;

    [SerializeField] StudioEventEmitter notaSoundEmitter;
    [SerializeField] StudioEventEmitter aliadoEmitter;

    [SerializeField] StudioEventEmitter arrastrarCaja;

    [SerializeField] EventReference recogerArma;
    [SerializeField] EventReference recogerBalas;

    [SerializeField] EventReference ativarCheckpoint;

    [SerializeField] EventReference aguaSplash;
    private void OnEnable()
    {        
        SoundEvents.DestruirObjeto += ReproducirDestruirObjeto;

        SoundEvents.RecogerNota += RecogerNota;
        SoundEvents.HablarAliadoNPC += ReproducirAliadoNPC;

        SoundEvents.RecogerArma += RecogerArma;
        SoundEvents.RecogerBalas += RecogerBalas;

        SoundEvents.ArrastrarObjeto += ArrastrarObjeto;
        SoundEvents.DetenerArrastrarObjeto += DetenerArrastrarObjeto;

        SoundEvents.CheckpointActivado += ActivarCheckpoint;

        SoundEvents.CaerAgua += ReproducirSplash;
    }


    //SONIDO DE DESTRUIR CAJA
    public void ReproducirDestruirObjeto(float posicionObjeto, int tipo)
    {
        
        if (destruirObjetoEmitter != null)
        {
        destruirObjetoEmitter.Play();

            float distancia = ataquePersonaje.transform.position.x - posicionObjeto;
            //Debug.Log("Distancia: " + distancia);

            float distNormalizado = distancia / 8;
            //Debug.Log("Distancia Normalizada: " + distNormalizado);
            destruirObjetoEmitter.EventInstance.setParameterByName("Panner", -(distNormalizado));
            destruirObjetoEmitter.EventInstance.setParameterByName("Tipo", tipo);
            //Debug.Log("Valor actual del emiter: " + tipo);

            
        }
    }

    public void RecogerNota()
    { 
        if (notaSoundEmitter != null)
        {
            notaSoundEmitter.Play();
        }
    }

    public void ReproducirAliadoNPC()
    {
        if (aliadoEmitter != null)
        {

            aliadoEmitter.Play();

            int ultimoValor = -1; 
            int NuevoRandom()
            {
                int nuevo;
                do
                {
                    nuevo = UnityEngine.Random.Range(0, 4);
                } while (nuevo == ultimoValor);

                ultimoValor = nuevo;
                return nuevo;
            }
            aliadoEmitter.EventInstance.setParameterByName("RandomAllyNPC", NuevoRandom());
            aliadoEmitter.EventInstance.getParameterByName("RandomAllyNPC", out float test);
            //Debug.Log("Valor Random: " + NuevoRandom());
            //Debug.Log("Valor actual del emiter: " + test);
        }
    }

    public void RecogerArma()
    {
        if (!recogerArma.IsNull)
            RuntimeManager.PlayOneShot(recogerArma);
    }
    public void RecogerBalas()
    {
        if (!recogerBalas.IsNull)
            RuntimeManager.PlayOneShot(recogerBalas);
    }
    public void ActivarCheckpoint()
    {
        if (!ativarCheckpoint.IsNull)
            RuntimeManager.PlayOneShot(ativarCheckpoint);
    }

    public void ArrastrarObjeto()
    {
        if (arrastrarCaja != null)
            arrastrarCaja.Play();
    }

    public void DetenerArrastrarObjeto()
    {
        if (arrastrarCaja != null)
            arrastrarCaja.Stop();
    }

    public void ReproducirSplash()
    {
        if (!aguaSplash.IsNull) {
            RuntimeManager.PlayOneShot(aguaSplash);
        }
    }

}
