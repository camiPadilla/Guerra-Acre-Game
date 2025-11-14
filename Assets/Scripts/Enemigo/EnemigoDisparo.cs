using System.Collections;
using UnityEngine;

public class EnemigoDisparo : Enemigo_IA
{
    [Header("Disparo")]

    [Header("Disparo")]
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private GameObject piedraPrefab;
    [SerializeField] private Transform puntoDisparoBala;
    [SerializeField] private Transform puntoDisparoPiedra;
    [SerializeField] private int nroBalas = 15;
    [SerializeField] private int nroPiedras = 5;
    [SerializeField] private float distanciaOptima = 5f;
    [SerializeField] private float tolerancia = 3f;
    [SerializeField] private bool followPlayer = true;

    [Header("Fusil o Piedra")]
    [SerializeField] private bool fusil;
    private bool puedeDisparar = true;

    public override void Atacar()
    {
        if (jugador == null) return;

        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        Flip(jugador.position.x > transform.position.x);

        if (followPlayer)
            Posicionarse(distanciaJugador);

        if (Mathf.Abs(distanciaJugador - distanciaOptima) <= tolerancia && puedeDisparar)
        {
            if (fusil)
                StartCoroutine(DispararFusil());
            else
                StartCoroutine(LanzarPiedra());
        }
    }
    private IEnumerator LanzarPiedra()
    {
        puedeDisparar = false;

        if (nroPiedras > 0 && jugador != null)
        {
            GameObject piedra = Instantiate(piedraPrefab, puntoDisparoPiedra.position, Quaternion.identity);
            PiedraEnemigo p = piedra.GetComponent<PiedraEnemigo>();
            if (p != null)
                p.Inicializar(jugador);

            nroPiedras--;
        }

        yield return new WaitForSeconds(1.2f);
        puedeDisparar = true;
    }
    private IEnumerator DispararFusil()
    {
        puedeDisparar = false;

        if (nroBalas > 0 && jugador != null)
        {
            GameObject bala = Instantiate(balaPrefab, puntoDisparoBala.position, Quaternion.identity);
            BalaEnemigo b = bala.GetComponent<BalaEnemigo>();
            if (b != null) b.Inicializar(jugador);
            nroBalas--;
            SoundEvents.DisparoEnemigo.Invoke(transform.position.x);
        }

        yield return new WaitForSeconds(0.8f);
        puedeDisparar = true;
    }

    private void Posicionarse(float distanciaJugador)
    {
        float diferencia = distanciaJugador - distanciaOptima;

        if (Mathf.Abs(diferencia) > tolerancia)
        {
            float dir = Mathf.Sign(jugador.position.x - transform.position.x);
            rbEnemigo.velocity = new Vector2(dir * speed, rbEnemigo.velocity.y);
        }
        else
        {
            rbEnemigo.velocity = Vector2.zero;
        }
    }
}
