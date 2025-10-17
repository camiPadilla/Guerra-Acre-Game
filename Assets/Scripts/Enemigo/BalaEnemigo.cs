using System.Collections;
using UnityEngine;
using TarodevController;
using System;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocidadBala;
    [SerializeField] public int damage = 2;
    private Transform jugador;
    [SerializeField] private GameObject enemigo;

    public void Inicializar(Transform jugadorDestino)
    {
        jugador = jugadorDestino;
        Disparar();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Disparar()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        rb.velocity = new Vector2(direccion.x * velocidadBala, direccion.y * velocidadBala);
        StartCoroutine(DestruirBala());
    }

    private IEnumerator DestruirBala()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<SaludPersonaje>().PerderVida(damage);
             Destroy(gameObject);
        }
        if (collision.transform.CompareTag("Enemigo"))
        {
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
       
    }
}
