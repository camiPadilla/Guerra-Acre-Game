using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] StudioEventEmitter marchaMilitarEmitter;

    private void OnEnable()
    {
        //SoundEvents.MorirMosquito += ReproducirMorirMosquito;

    }

    /*public void ReproducirMorirMosquito()
    {
        if (morirMosquitoEmitter != null)
        {
            morirMosquitoEmitter.Play();
        }
    }*/
}
