using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

//Ya que para disparar y lanzar piedras es lo mismo, solo cambia el objeto que lanza, entonces prefiero usar el mismo codigo
//y solo cambiar el objeto que lanza en el inspector (Sugeto a camabios, ya que creo que se aplicara una fuerza para lanzar piedras)
//en ese caso se verificara que tipo de objeto es y se le aplicara la fuerza
public class EnemigoDisparo : Enemigo_IA
{
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private int nroBalas;
    [SerializeField] private float tiempoEntreDisparos = 1.5f; // segundos
    [SerializeField] private float distanciaOptima = 3f;

    private float cooldownDisparo = 0f;

    public override void Atacar()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador se sale del rango → patrullaje
        if (distanciaJugador > rangoVision)
        {
            PatrullajeIA();
            return;
        }

        // Posicionarse (mantener cierta distancia)
        Posicionarse(distanciaJugador);

    }

    private void Shoot()
    {
        if (cooldownDisparo <= 0 && nroBalas > 0)
        {
            Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
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

        // Mantenerse a una distancia optima
        if (distanciaJugador > distanciaOptima)
        {
            // Acercarse
            Debug.Log("Acercarse");
            transform.position = Vector2.MoveTowards(transform.position, jugador.position, speed * Time.deltaTime);
        }
        else
        {
            //Shoot();
            print("Ya me posicione :D");
        }
        if (distanciaJugador < distanciaOptima - 1f) // margen para no estar encima
        {
            print(distanciaJugador);
            // Alejarse
            print("Alejarse");
            Vector2 direccion = (transform.position - jugador.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direccion, speed * Time.deltaTime);
        }
    }
}
