using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AtaquePersonaje ataquePersonaje;
    [SerializeField] Proyectil piedra;

    [SerializeField] StudioEventEmitter pasosPastoEmitter;
    [SerializeField] StudioEventEmitter saltarEmitter;
    [SerializeField] StudioEventEmitter cargarpiedraEmitter;
    [SerializeField] StudioEventEmitter lanzarpiedraEmitter;
    [SerializeField] StudioEventEmitter destruirCajaEmitter;
    [SerializeField] StudioEventEmitter notaSoundEmitter;
    [SerializeField] StudioEventEmitter aliadoEmitter;

    private void OnEnable()
    {
        SoundEvents.CargarFuerzaPiedra += ReproducirCargarPiedra;
        SoundEvents.DetenerCarga += DetenerCarga;

        SoundEvents.LanzarPiedra += ReproducirLanzarPiedra;

        SoundEvents.DestruirCaja += ReproducirDestruirCaja;

        SoundEvents.Salto += ReproducirSalto;
        SoundEvents.PasosPasto += ReproducirPasos;
        SoundEvents.DetenerPasosPasto += DetenerPasos;

        SoundEvents.RecogerNota += RecogerNota;
        SoundEvents.HablarAliadoNPC += ReproducirAliadoNPC;
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



    //SONIDO DE DESTRUIR CAJA
    public void ReproducirDestruirCaja(float posicionCaja)
    {
        
        if (destruirCajaEmitter != null)
        {
            destruirCajaEmitter.Play();

            float distancia = ataquePersonaje.transform.position.x - posicionCaja;
            //Debug.Log("Distancia: " + distancia);

            float distNormalizado = distancia / 8;
            //Debug.Log("Distancia Normalizada: " + distNormalizado);
            destruirCajaEmitter.EventInstance.setParameterByName("CajaPanner", -(distNormalizado));
            destruirCajaEmitter.EventInstance.getParameterByName("CajaPanner", out float test);
            //Debug.Log("Valor actual del emiter: " + test);

            
        }
    }

    //SONIDO DE SALTO
public void ReproducirSalto(float parametro)
    {
        if (saltarEmitter != null)
        {            
            Debug.Log("Reproduciendo salto");   
            
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
                    nuevo = UnityEngine.Random.Range(0, 3);
                } while (nuevo == ultimoValor);

                ultimoValor = nuevo;
                return nuevo;
            }
            aliadoEmitter.EventInstance.setParameterByName("RandomAllyNPC", NuevoRandom());
            saltarEmitter.EventInstance.getParameterByName("RandomAllyNPC", out float test);
            //Debug.Log("Valor Random: " + NuevoRandom());
            //Debug.Log("Valor actual del emiter: " + test);
        }
    }
}
