using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocidadBala = 10f;
    [SerializeField] private float tiempoVida = 3f;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // La bala se destruye sola después de un tiempo
        Destroy(gameObject, tiempoVida);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la bala colisiona con el jugador o con cualquier cosa que no sea el enemigo
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    // Se llama al instanciar la bala para darle dirección
    public void DireccionBala(float drecEn)
    {
        float direccion = Mathf.Sign(drecEn);

        // Importante: desactivar la gravedad
        rb.gravityScale = 0;

        // Asignar velocidad una vez
        rb.velocity = new Vector2(direccion * velocidadBala, 0f);
    }
}
