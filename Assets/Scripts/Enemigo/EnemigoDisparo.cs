using System.Collections;
using UnityEngine;

public class EnemigoDisparo : Enemigo_IA
{
    [Header("Disparo")]
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private GameObject piedraPrefab;
    [SerializeField] private Transform puntoDisparoBala;
    [SerializeField] private Transform puntoDisparoPiedra;
    [SerializeField] private int nroBalas = 15;
    [SerializeField] private int nroPiedras = 5;
    [SerializeField] private float distanciaOptima = 5f; // distancia ideal para disparar
    [SerializeField] private float tolerancia = 3f;     // margen para no moverse tanto

    [Header("Fusil o Piedra")]
    [SerializeField] private bool fusil; // true = fusil, false = piedra
    private bool puedeDisparar = true;

    public override void Atacar()
    {
        if (jugador == null) return;

        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        // Girar siempre hacia el jugador
        Flip(jugador.position.x > transform.position.x);

        // Posicionarse a la distancia adecuada
        Posicionarse(distanciaJugador);

        // Si está dentro del rango óptimo, dispara
        if (Mathf.Abs(distanciaJugador - distanciaOptima) <= tolerancia && puedeDisparar)
        {
            if (fusil)
                StartCoroutine(DispararFusil());
            else
                StartCoroutine(LanzarPiedra());
        }
    }

    private IEnumerator DispararFusil()
    {
        puedeDisparar = false;

        if (nroBalas > 0)
        {
            GameObject nuevaBala = Instantiate(balaPrefab, puntoDisparoBala.position, puntoDisparoBala.rotation);
            BalaEnemigo bala = nuevaBala.GetComponent<BalaEnemigo>();
            if (bala != null) bala.Inicializar(jugador);
            nroBalas--;
        }

        yield return new WaitForSeconds(1f);
        puedeDisparar = true;
    }

    private IEnumerator LanzarPiedra()
    {
        puedeDisparar = false;

        if (nroPiedras > 0)
        {
            GameObject piedra = Instantiate(piedraPrefab, puntoDisparoPiedra.position, Quaternion.identity);
            PiedraEnemigo p = piedra.GetComponent<PiedraEnemigo>();
            if (p != null) p.Inicializar(jugador);

            nroPiedras--;
        }

        yield return new WaitForSeconds(1f);
        puedeDisparar = true;
    }

    private void Posicionarse(float distanciaJugador)
{
    float diferencia = distanciaJugador - distanciaOptima;

    if (Mathf.Abs(diferencia) > tolerancia)
    {
        float direccion = Mathf.Sign(jugador.position.x - transform.position.x);
        rbEnemigo.velocity = new Vector2(direccion * speed, rbEnemigo.velocity.y);
    }
    else
    {
        rbEnemigo.velocity = Vector2.zero;
    }
}
}
