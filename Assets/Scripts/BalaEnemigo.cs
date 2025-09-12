using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int velocidadBala;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la bala colisiona con el jugador, destruir la bala
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Asigna la dirección (1 = derecha, -1 = izquierda)
    public void DireccionBala(float drecEn)
    {
        float direccion = Mathf.Sign(drecEn);
        rb.velocity = new Vector2(direccion * velocidadBala, 0f);
    }
}
