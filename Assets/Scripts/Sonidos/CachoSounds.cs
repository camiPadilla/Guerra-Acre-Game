using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class CachoSounds : MonoBehaviour
{
    [SerializeField] EventReference anotarSFX;
    [SerializeField] EventReference lanzarDadoSFX;
    [SerializeField] EventReference hoverCachoSFX;

    private EventInstance instanciaAnotar;
    private EventInstance lanzarDado;
    private EventInstance hoverCacho;

    void Start()
    {
        instanciaAnotar = RuntimeManager.CreateInstance(anotarSFX);
        lanzarDado = RuntimeManager.CreateInstance(lanzarDadoSFX);
        hoverCacho = RuntimeManager.CreateInstance(hoverCachoSFX);
    }
    private void OnEnable()
    {
        SoundEvents.anotar += ReproducirAnotar;
        SoundEvents.lanzarDado += ReproducirLanzarDado;
        SoundEvents.hoverCacho += ReproducirHoverCacho;
    }
    public void ReproducirAnotar()
    {
        if (!anotarSFX.IsNull)
        {
            instanciaAnotar.start();
        }
    }
    public void ReproducirLanzarDado()
    {
        if (!lanzarDadoSFX.IsNull)
        {
            lanzarDado.start();
        }
    }
    public void ReproducirHoverCacho()
    {
        if (!hoverCachoSFX.IsNull)
        {
            hoverCacho.start();
        }
    }
}
