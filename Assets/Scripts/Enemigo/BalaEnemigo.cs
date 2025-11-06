using System.Collections;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocidadBala = 6f;
    [SerializeField] public int damage = 2;
    private Transform jugador;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Inicializar(Transform jugadorDestino)
    {
        jugador = jugadorDestino;
        Disparar();
    }

    private void Disparar()
    {
        if (jugador == null)
        {
            Debug.LogWarning("No se asignó jugador a la bala enemiga.");
            return;
        }

        // Direccion hacia el jugador
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // Asignar velocidad en esa dirección
        rb.velocity = direccion * velocidadBala;

        // Rotar la bala visualmente hacia el jugador (opcional)
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        StartCoroutine(DestruirBala());
    }

    private IEnumerator DestruirBala()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SaludPersonaje salud = collision.GetComponent<SaludPersonaje>();
            if (salud != null)
                salud.PerderVida(damage);

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemigo"))
        {
            // Evita colisiones con el enemigo que disparó
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
    }
}