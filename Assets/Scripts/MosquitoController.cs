using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MosquitoController : MonoBehaviour
{
    [SerializeField] Transform PosicionJugador;
    bool patrullando = true;
    [SerializeField] int probabilidad;
    [SerializeField] int velocidad;
    Vector2 posicionAleatoria;
    Vector2 posicionInicial;
    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
        cambiarPosicion();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            patrullando = true;
            collision.gameObject.SendMessageUpwards("PerderVida");
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
