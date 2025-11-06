using System.Collections;
using UnityEngine;
using TarodevController;
using System;

public class BalaEnemigo : MonoBehaviour
{
    [Header("Configuración de bala")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocidadBala = 5f;
    [SerializeField] public int damage = 2;
    [SerializeField] private float tiempoVida = 5f;

    [Header("Referencias")]
    private Transform jugador;
    private Vector2 direccion;

    public void Start()
    {
        jugador = FindAnyObjectByType<PlayerController>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Disparar()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        rb.velocity = direccion * velocidadBala;
        StartCoroutine(DestruirBala());
    }

    private IEnumerator DestruirBala()
    {
        yield return new WaitForSeconds(tiempoVida);
        if (gameObject != null)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignora colisiones con el enemigo que la disparó
        if (collision.CompareTag("Enemigo"))
        {
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
            return;
        }

        if (collision.CompareTag("Player"))
        {
            var salud = collision.GetComponent<SaludPersonaje>();
            if (salud != null)
                salud.PerderVida(damage);

            Destroy(gameObject);
        }
    }
}