using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;
using UnityEngine.UIElements;
using TarodevController;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AtaquePersonaje ataquePersonaje;
    [SerializeField] PlayerController playerController;

    [SerializeField] StudioEventEmitter pasosPastoEmitter;
    [SerializeField] StudioEventEmitter saltarEmitter;
    [SerializeField] StudioEventEmitter cargarpiedraEmitter;
    [SerializeField] StudioEventEmitter lanzarpiedraEmitter;
    [SerializeField] StudioEventEmitter macheteEmitter;
    [SerializeField] EventReference activarMachete;
    [SerializeField] EventReference activarPiedra;
    [SerializeField] EventReference sinBalas;
    [SerializeField] EventReference recargaBalas;
    [SerializeField] EventReference dano;
    [SerializeField] EventReference muerte;

    private EventInstance instanciaMuerte;

    private void OnEnable()
    {
        SoundEvents.CargarFuerzaPiedra += ReproducirCargarPiedra;
        SoundEvents.DetenerCarga += DetenerCarga;
        SoundEvents.AtaqueMachete += ReproducirMachete;

        SoundEvents.LanzarPiedra += ReproducirLanzarPiedra;

        SoundEvents.Salto += ReproducirSalto;
        SoundEvents.PasosPasto += ReproducirPasos;
        SoundEvents.DetenerPasosPasto += DetenerPasos;
        SoundEvents.CambiarArmaMachete += ActivarMacheteSonido;
        SoundEvents.CambiarArmaPiedra += ActivarPiedraSonido;

        SoundEvents.SinBalas += ReproducirSinBalas;
        SoundEvents.RecargarBalas += ReproducirRecargaBalas;

        SoundEvents.MorirPersonaje += ReproducirMuerte;
        SoundEvents.DanoPersonaje += ReproducirDano;
    }

    private void Start()
    {
        instanciaMuerte = RuntimeManager.CreateInstance(muerte);
    }
    void Update()
    {
        if (pasosPastoEmitter != null && pasosPastoEmitter.IsPlaying())
        {
            pasosPastoEmitter.EventInstance.setParameterByName("Gait", playerController.gait);
        }
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

            //pasosPastoEmitter.EventInstance.setParameterByName("Gait", playerController.gait);
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

    public void ReproducirMachete()
    {
        if (macheteEmitter != null)
        {
            macheteEmitter.Play();
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
            macheteEmitter.EventInstance.setParameterByName("RandomMelee", NuevoRandom());
        }
    }

    public void ActivarMacheteSonido()
    {
        if (!activarMachete.IsNull)
        {
            RuntimeManager.PlayOneShot(activarMachete);
        }
    }
    public void ActivarPiedraSonido()
    {
        if (!activarPiedra.IsNull)
        {
            RuntimeManager.PlayOneShot(activarPiedra);
        }
    }
    public void ReproducirSinBalas()
    {
        if (!sinBalas.IsNull)
        {
            RuntimeManager.PlayOneShot(sinBalas);
        }
    }
    public void ReproducirRecargaBalas()
    {
        if (!recargaBalas.IsNull)
        {
            RuntimeManager.PlayOneShot(recargaBalas);
        }
    }
    public void ReproducirMuerte()
    {
        if (!muerte.IsNull)
        {
            instanciaMuerte.start();
            int ultimoValor = -1;
            int NuevoRandom()
            {
                int nuevo;
                do
                {
                    nuevo = UnityEngine.Random.Range(0, 1);
                } while (nuevo == ultimoValor);

                ultimoValor = nuevo;
                return nuevo;
            }
            instanciaMuerte.setParameterByName("Random0-1", NuevoRandom());
        }
    }
    public void ReproducirDano()
    {
        if (!dano.IsNull)
        {
            RuntimeManager.PlayOneShot(dano);
        }
    }
}