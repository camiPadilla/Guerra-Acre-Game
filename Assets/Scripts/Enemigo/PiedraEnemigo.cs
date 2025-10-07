using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiedraEnemigo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float fuerzaX = 5f;  // fuerza horizontal
    [SerializeField] private float fuerzaY = 5f;  // fuerza vertical extra
    [SerializeField] private Transform jugador;
    [SerializeField] private int damage = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = FindAnyObjectByType<MovPersonaje>().transform;
    }

    private IEnumerator DestruirPiedra()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void Lanzar()
    {
        if (jugador == null) return;

        // Direccion hacia el jugador
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // Aplicamos un impulso (horizontal hacia jugador + vertical extra)
        Vector2 fuerza = new Vector2(direccion.x * fuerzaX, fuerzaY);
        rb.AddForce(fuerza, ForceMode2D.Impulse);

        StartCoroutine(DestruirPiedra());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("PerderVida", damage);
            Destroy(gameObject);
        }
        
        
    }
}
