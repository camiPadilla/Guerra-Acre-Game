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

    [SerializeField] StudioEventEmitter destruirCajaEmitter;
    [SerializeField] StudioEventEmitter notaSoundEmitter;
    [SerializeField] StudioEventEmitter aliadoEmitter;

    private void OnEnable()
    {        
        SoundEvents.DestruirCaja += ReproducirDestruirCaja;

        SoundEvents.RecogerNota += RecogerNota;
        SoundEvents.HablarAliadoNPC += ReproducirAliadoNPC;
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
            aliadoEmitter.EventInstance.getParameterByName("RandomAllyNPC", out float test);
            //Debug.Log("Valor Random: " + NuevoRandom());
            //Debug.Log("Valor actual del emiter: " + test);
        }
    }
}
