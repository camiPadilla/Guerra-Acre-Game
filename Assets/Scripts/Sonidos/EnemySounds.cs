using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] StudioEventEmitter morirMosquitoEmitter;
        
    private void OnEnable()
    {
        SoundEvents.MorirMosquito += ReproducirMorirMosquito;

    }

    public void ReproducirMorirMosquito()
    {
        if (morirMosquitoEmitter != null)
        {
            morirMosquitoEmitter.Play();
        }
    }
}
