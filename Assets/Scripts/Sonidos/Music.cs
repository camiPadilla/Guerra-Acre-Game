using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    [SerializeField] EventReference musicNivel_1;
    [SerializeField] EventReference musicNivel_2;

    private void OnEnable()
    {

    }
    void Start()
    {

        Scene escenaActiva = SceneManager.GetActiveScene();

        if (escenaActiva.name == "EscenaUno")
        {
            RuntimeManager.PlayOneShot(musicNivel_1);
        }
        else if (escenaActiva.name == "EscenaDos")
        {
            RuntimeManager.PlayOneShot(musicNivel_2);
        }
        Debug.Log("La escena activa es: " + escenaActiva.name);
    }


}
