using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaRespawn : MonoBehaviour
{
    [SerializeField]
    Transform puntoRespawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SoundEvents.CaerAgua?.Invoke(); //Sonido by Chelo :D
            SaludPersonaje personaje = collision.gameObject.GetComponent<SaludPersonaje>();
            personaje.PerderVida(1);
            personaje.RegresarCheckPoint();
            //personaje.transform.position = puntoRespawn.position;
            Debug.Log("checkpoint");
        }
    }

}
