using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class AguaRespawn : MonoBehaviour
{
    [SerializeField]
    Transform puntoRespawn;

    StudioEventEmitter rioSound;
    [SerializeField] Transform PosicionJugador;
    // Start is called before the first frame update
    void Start()
    {
        rioSound = GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rioSound != null && PosicionJugador != null) // ADDED BY CHELO :D
        {
            float distancia = PosicionJugador.position.x - transform.position.x;
            float distNormalizado = Mathf.Clamp(distancia / 8, -1f, 1f);
            rioSound.EventInstance.setParameterByName("RioPanner", -distNormalizado);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SoundEvents.CaerAgua?.Invoke(); //Sonido by Chelo :D
            SaludPersonaje personaje = collision.gameObject.GetComponent<SaludPersonaje>();
            personaje.PerderVida(1);
            personaje.DesactivarInvulnerabilidad();
            personaje.RegresarCheckPoint();
            //personaje.transform.position = puntoRespawn.position;
            Debug.Log("checkpoint");
        }
    }

}
