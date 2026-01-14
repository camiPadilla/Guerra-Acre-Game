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

    [SerializeField] private float distanciaOptima = 5f;
    [SerializeField] private float tolerancia = 1f;

    [SerializeField] private bool followPlayer = true;
    [SerializeField] private bool fusil = true;

    private bool puedeDisparar = true;

    public override void Atacar()
    {
        if (jugador == null) return;

        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        // Siempre mirar al jugador
        Flip(jugador.position.x > transform.position.x);

        // Controlar distancia óptima
        if (followPlayer)
            Posicionarse(distanciaJugador);

        // --- CONDICIÓN FINAL PARA DISPARAR ---
        // Está en rango óptimo Y puede disparar → dispara
        bool dentroRango = Mathf.Abs(distanciaJugador - distanciaOptima) <= tolerancia;

        if (dentroRango && puedeDisparar)
        {
            if (fusil)
                StartCoroutine(DispararFusil());
            else
                StartCoroutine(LanzarPiedra());
        }
    }

    // --- DISPARO DE PIEDRA ---
    private IEnumerator LanzarPiedra()
    {
        puedeDisparar = false;

        if (nroPiedras > 0)
        {
            GameObject piedra = Instantiate(piedraPrefab, puntoDisparoPiedra.position, puntoDisparoPiedra.rotation);
            PiedraEnemigo p = piedra.GetComponent<PiedraEnemigo>();
            if (p != null) p.Inicializar(jugador);

            nroPiedras--;
        }

        yield return new WaitForSeconds(1.2f);
        puedeDisparar = true;
    }

    // --- DISPARO DE FUSIL ---
    private IEnumerator DispararFusil()
    {
        puedeDisparar = false;

        if (nroBalas > 0)
        {
            GameObject bala = Instantiate(balaPrefab, puntoDisparoBala.position, puntoDisparoBala.rotation);
            BalaEnemigo b = bala.GetComponent<BalaEnemigo>();
            if (b != null) b.Inicializar(jugador);
            SoundEvents.DisparoEnemigo?.Invoke(transform.position.x); //Sound By Chelo
            //nroBalas--;
        }

        yield return new WaitForSeconds(2f);
        puedeDisparar = true;
    }

    // --- CONTROL DE DISTANCIA ---
    private void Posicionarse(float distanciaJugador)
    {
        float diferencia = distanciaJugador - distanciaOptima;

        // Está lejos → ACERCARSE
        if (diferencia > tolerancia)
        {
            float dir = Mathf.Sign(jugador.position.x - transform.position.x);
            rbEnemigo.velocity = new Vector2(dir * speed, rbEnemigo.velocity.y);
        }
        // Está muy cerca → ALEJARSE
        else if (diferencia < -tolerancia)
        {
            float dir = -Mathf.Sign(jugador.position.x - transform.position.x);
            rbEnemigo.velocity = new Vector2(dir * speed, rbEnemigo.velocity.y);
        }
        else
        {
            // En distancia óptima → quedarse quieto para disparar
            rbEnemigo.velocity = new Vector2(0, rbEnemigo.velocity.y);
        }
    }
}