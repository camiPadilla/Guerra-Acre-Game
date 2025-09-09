using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

//Ya que para disparar y lanzar piedras es lo mismo, solo cambia el objeto que lanza, entonces prefiero usar el mismo codigo
//y solo cambiar el objeto que lanza en el inspector (Sugeto a camabios, ya que creo que se aplicara una fuerza para lanzar piedras)
//en ese caso se verificara que tipo de objeto es y se le aplicara la fuerza
public class EnemigoDisparo : Enemigo_IA
{
    [Header("Disparo")]
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private GameObject piedraPrefab;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private int nroBalas = 10;
    [SerializeField] private float tiempoEntreDisparos = 1.5f; 
    [SerializeField] private float distanciaOptima = 5f; // distancia ideal para disparar
    [SerializeField] private float tolerancia = 1f;      // margen para no moverse tanto
    
    [Header("fusil o piedra")]
    [SerializeField] private bool fusil; //para ver si es piedra o fusil para atacar de manera diferente
    private float cooldownDisparo = 3f;

    public override void Atacar()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador se sale del rango → volver a patrullaje
        if (distanciaJugador > rangoVision)
        {
            print ("Volviendo a patrullar");
            PatrullajeIA();
            return;
        }

        // Posicionarse a la distancia adecuada
        Posicionarse(distanciaJugador);

        // Si ya está en rango de disparo → disparar
        if (Mathf.Abs(distanciaJugador - distanciaOptima) <= tolerancia)
        {
            if (fusil)
            {
                Fusil();
            }
            else
            {
                Piedra();
            }
        }
    }
    private void Fusil()
    {
        //corregir, la bala no se esta impulsando
        tiempoEntreDisparos = 2;
        if (cooldownDisparo <= 0f && nroBalas > 0)
        {
            Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
            nroBalas--;
            cooldownDisparo = tiempoEntreDisparos;
            balaPrefab.GetComponent<BalaEnemigo>().DireccionBala(transform.localScale.x);
        }
        else
        {
            cooldownDisparo -= Time.deltaTime;
        }
    }
    private void Piedra()
    {
        tiempoEntreDisparos = 4;
        if (cooldownDisparo <= 0f && nroBalas > 0)
        {
            Instantiate(piedraPrefab, puntoDisparo.position, puntoDisparo.rotation);
            nroBalas--;
            cooldownDisparo = tiempoEntreDisparos;
        }
        else
        {
            cooldownDisparo -= Time.deltaTime;
        }
    }

    private void Posicionarse(float distanciaJugador)
    {
        // Girar hacia el jugador
        Flip(jugador.position.x > transform.position.x);

        // Si el jugador esta cerca, el enemigo se aleja
        if (distanciaJugador < distanciaOptima - tolerancia)
        {
            float direccion = Mathf.Sign(transform.position.x - jugador.position.x);
            rbEnemigo.velocity = new Vector2(direccion * speed, rbEnemigo.velocity.y);
        }
        // Si el jugador esta lejos, el enemigo se acerca
        else if (distanciaJugador > distanciaOptima + tolerancia)
        {
            float direccion = Mathf.Sign(jugador.position.x - transform.position.x);
            rbEnemigo.velocity = new Vector2(direccion * speed, rbEnemigo.velocity.y);
        }
        // Si ya está en rango óptimo → detenerse
        else
        {
            rbEnemigo.velocity = Vector2.zero;
        }
    }
}
