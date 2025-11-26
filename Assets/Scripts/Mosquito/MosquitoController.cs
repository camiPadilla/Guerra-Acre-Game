using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Animations;

public class MosquitoController : MonoBehaviour
{
    [SerializeField] Transform PosicionJugador;
    bool patrullando = true;
    [SerializeField] int probabilidad;
    [SerializeField] int velocidad;
    [SerializeField] int damage;
    Vector2 posicionAleatoria;
    Vector2 posicionInicial;
    StudioEventEmitter vueloMosquito;


    // Start is called before the first frame update
    void Start()
    {
        vueloMosquito = GetComponent<StudioEventEmitter>();
        posicionInicial = transform.position;
        cambiarPosicion();
        if(PosicionJugador == null)
        {
            PosicionJugador = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (vueloMosquito != null && PosicionJugador != null) // ADDED BY CHELO :D
        {
            float distancia = PosicionJugador.position.x - transform.position.x;
            float distNormalizado = Mathf.Clamp(distancia / 8, -1f, 1f);
            vueloMosquito.EventInstance.setParameterByName("PannerMosquito", -distNormalizado);
            
            float distanceY = PosicionJugador.position.y - transform.position.y;
            float distNormalY = Mathf.Clamp(distanceY / 8, -1f, 1f);
            vueloMosquito.EventInstance.setParameterByName("DistanceY", -distNormalY);
        }


        picar();
        if (!patrullando)
        {

            transform.position = Vector2.MoveTowards(transform.position, PosicionJugador.position, velocidad * Time.deltaTime);
        }
        else
        {
            if (Vector2.Distance(posicionAleatoria, transform.position) > 0.7f)
            {
                transform.position = Vector2.MoveTowards(transform.position, posicionAleatoria, velocidad * Time.deltaTime);
            }
            else
            {
                cambiarPosicion();
            }
        }
    }

    void picar()
    {
        int numeroRandom = Random.Range(0, probabilidad);
        //Debug.Log(numeroRandom);
        if (numeroRandom == 0 && Vector2.Distance(transform.position, PosicionJugador.position) < 5f)
        {
            patrullando = false;
            Debug.Log("listo para atacar");
            CambiarDireccion(PosicionJugador.position.x);

        }
        else
        {
            patrullando = true;
        }
    }
    void CambiarDireccion(float xobjetivo) { 
       if (transform.position.x > xobjetivo)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            patrullando = true;
            collision.gameObject.SendMessageUpwards("PerderVida", damage);
        }
        if (collision.transform.CompareTag("hitbox"))
        {
            Debug.Log("me toco algo que miedo");
            gameObject.SendMessage("perderVida");
        }
    }
    void cambiarPosicion()
    {
        posicionAleatoria = new Vector2(posicionInicial.x + Random.Range(-3f, 3f), posicionInicial.y + Random.Range(-2f, 1f));
        CambiarDireccion(posicionAleatoria.x);
    }
}
