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

    private void OnEnable()
    {        
        SoundEvents.DestruirObjeto += ReproducirDestruirObjeto;

        SoundEvents.RecogerNota += RecogerNota;
        SoundEvents.HablarAliadoNPC += ReproducirAliadoNPC;
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
}
