using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class AguaRespawn : MonoBehaviour
{
    [SerializeField]
    Transform puntoRespawn;
    public SaludPersonaje perso;
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
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SoundEvents.CaerAgua?.Invoke(); //Sonido by Chelo :D
            perso.PerderVida(1);
            perso.DesactivarInvulnerabilidad();
            perso.RegresarCheckPoint();
            //personaje.transform.position = puntoRespawn.position;
            Debug.Log("checkpoint");
        }
    }

}
