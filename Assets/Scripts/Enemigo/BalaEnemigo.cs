using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocidadBala = 10f;
    [SerializeField] private Transform jugador;
    [SerializeField] public int damage = 2;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = FindAnyObjectByType<MovPersonaje>().transform;
    }

    public void Disparar()
    {
        Vector2 direccionP = (jugador.position - transform.position).normalized;
        rb.velocity = direccionP * velocidadBala;
        StartCoroutine(DestruirBala());
    }
    IEnumerator DestruirBala()
    {
        float tiempoDes = 5f;
        yield return new WaitForSeconds(tiempoDes);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("PerderVida", damage);
        }
        Destroy(gameObject);
    }

}
