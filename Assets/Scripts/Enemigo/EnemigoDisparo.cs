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
    [SerializeField] private Transform puntoDisparoBala;
    [SerializeField] private Transform puntoDisparoPiedra;
    [SerializeField] private int nroBalas = 15;
    [SerializeField] private int nroPiedras = 5;
    [SerializeField] private float distanciaOptima = 7f; // distancia ideal para disparar
    [SerializeField] private float tolerancia = 1f;      // margen para no moverse tanto

    [Header("fusil o piedra")]
    [SerializeField] private bool fusil; //para ver si es piedra o fusil , y atacar de manera diferente
    private float cooldownDisparo = 5f;
    private bool puedeDisparar = true;

    public override void Atacar()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

    // Si el jugador se sale de su rango vuelve a patrullar
        if (distanciaJugador > rangoVision)
        {
            print("Volviendo a patrullar");
            PatrullajeIA();
            return;
        }
    // Girar siempre hacia el jugador
        Flip(jugador.position.x < transform.position.x);
    // Posicionarse a la distancia adecuada
        Posicionarse(distanciaJugador);

    // Si ya está en su rango dispara
        if (Mathf.Abs(distanciaJugador - distanciaOptima) <= tolerancia)
        {
            if (fusil)
            {
                Fusil();
                print("Disparando");
            }
            else
            {
                Piedra();
            }
        }
    }
    private void Fusil()
    {
        if (nroBalas > 0 && puedeDisparar)
        {
            StartCoroutine(DispararFusil());
        }
    }

    private IEnumerator DispararFusil()
    {
        puedeDisparar = false;

       yield return new WaitForSeconds(5f);
        GameObject nuevaBala = Instantiate(balaPrefab, puntoDisparoBala.position, puntoDisparoBala.rotation);
        nuevaBala.GetComponent<BalaEnemigo>().Inicializar(jugador);
        StartCoroutine(DispararFusil());
        puedeDisparar = true;
    }

    private void Piedra()
    {
        if (nroPiedras > 0 && puedeDisparar)
        {
            StartCoroutine(LanzarPiedra());
        }
    }

    private IEnumerator LanzarPiedra()
    {
        puedeDisparar = false;

        GameObject piedra = Instantiate(piedraPrefab, puntoDisparoPiedra.position, Quaternion.identity);
        piedra.GetComponent<PiedraEnemigo>().Inicializar(jugador);


        nroPiedras--;

        yield return new WaitForSeconds(cooldownDisparo);
        puedeDisparar = true;
    }

    private void Posicionarse(float distanciaJugador)
    {
        // Girar hacia el jugador
        Flip(jugador.position.x < transform.position.x);
        // Si el jugador está cerca, el enemigo se aleja
        if (distanciaJugador < distanciaOptima - tolerancia)
        {
            float direccion = Mathf.Sign(transform.position.x - jugador.position.x);
            rbEnemigo.velocity = new Vector2(direccion * speed, rbEnemigo.velocity.y);
        }
        // Si el jugador está lejos, el enemigo se acerca
        else if (distanciaJugador > distanciaOptima + tolerancia)
        {
            float direccion = Mathf.Sign(jugador.position.x - transform.position.x);
            rbEnemigo.velocity = new Vector2(direccion * speed, rbEnemigo.velocity.y);
        }
        // Si ya está en rango optimo de ataque, s
        else
        {
            rbEnemigo.velocity = Vector2.zero;
        }
    }
}
