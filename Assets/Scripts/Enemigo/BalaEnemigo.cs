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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("PerderVida", damage, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject== enemigo)
        {
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
        }
    }
}
