using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;
using UnityEngine.UIElements;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] AtaquePersonaje ataquePersonaje;

    [SerializeField] StudioEventEmitter pasosPastoEmitter;
    [SerializeField] StudioEventEmitter saltarEmitter;
    [SerializeField] StudioEventEmitter cargarpiedraEmitter;
    [SerializeField] StudioEventEmitter lanzarpiedraEmitter;

    private void OnEnable()
    {
        SoundEvents.CargarFuerzaPiedra += ReproducirCargarPiedra;
        SoundEvents.DetenerCarga += DetenerCarga;

        SoundEvents.LanzarPiedra += ReproducirLanzarPiedra;

        SoundEvents.Salto += ReproducirSalto;
        SoundEvents.PasosPasto += ReproducirPasos;
        SoundEvents.DetenerPasosPasto += DetenerPasos;
    }

    //SONIDO DE CARGA
    public void ReproducirCargarPiedra()
    {
        if (cargarpiedraEmitter != null)
        {
            cargarpiedraEmitter.Play();
            cargarpiedraEmitter.EventInstance.setParameterByName("Paner", -(ataquePersonaje.dirX));
            StartCoroutine(IniciarPanearCarga());
        }
    }
    public IEnumerator IniciarPanearCarga()
    {
        if (cargarpiedraEmitter != null && cargarpiedraEmitter.EventInstance.isValid())
        {
            while (ataquePersonaje.fuerzatiro < ataquePersonaje.fuerzaMaxima
                && Input.GetMouseButton(0))
            {
                float normalizado = ((ataquePersonaje.fuerzaMaxima - ataquePersonaje.fuerzatiro)
                    / (ataquePersonaje.fuerzaMaxima)) * (ataquePersonaje.dirX * -1);

                cargarpiedraEmitter.EventInstance.setParameterByName("Paner", normalizado);
                cargarpiedraEmitter.EventInstance.getParameterByName("Paner", out float test);
                /*Debug.Log("Valor actual del emiter: " + test);
                Debug.Log("Valor fuerza: " + ataquePersonaje.fuerzatiro);
                Debug.Log("Valor normalizado: " + normalizado);*/
                yield return null;
            }
        }
    }
    public void DetenerCarga()
    {
        if (cargarpiedraEmitter != null)
        {
            cargarpiedraEmitter.Stop();
        }
    }



    //SONIDO DE LANZAMIENTO
    public void ReproducirLanzarPiedra()
    {
        if (lanzarpiedraEmitter != null)
        {
            lanzarpiedraEmitter.Play();
            lanzarpiedraEmitter.EventInstance.setParameterByName("Paner", -(ataquePersonaje.dirX));
        }
    }

    //SONIDO DE SALTO
    public void ReproducirSalto(float parametro)
    {
        if (saltarEmitter != null)
        {
            //Debug.Log("Reproduciendo salto");

            /*EventInstance salto = RuntimeManager.CreateInstance("event:/SFX/Gameplay/Player/Salto/Saltar");
            salto.start();            
            salto.release();*/
            saltarEmitter.Play();

            saltarEmitter.EventInstance.setParameterByName("Salto-Caida", parametro);
            saltarEmitter.EventInstance.getParameterByName("Salto-Caida", out float test);

            //Debug.Log("Salto Emitter Par: "+ test);
        }
    }

    //SONIDOS DE PASOS
    public void ReproducirPasos()
    {
        if (pasosPastoEmitter != null)
        {
            //Debug.Log("Reproduciendo pasos");
            pasosPastoEmitter.Play();
        }
    }
    public void DetenerPasos()
    {
        if (pasosPastoEmitter != null)
        {
            //Debug.Log("Pasos Detenidos");
            pasosPastoEmitter.Stop();
        }
    }
}
