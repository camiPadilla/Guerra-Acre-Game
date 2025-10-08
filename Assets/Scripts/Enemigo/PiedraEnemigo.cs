using System.Collections;
using UnityEngine;
using TarodevController;

public class PiedraEnemigo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float fuerzaX = 5f;
    [SerializeField] private float fuerzaY = 5f;
    [SerializeField] private int damage = 1;

    private Transform jugador;

    public void Inicializar(Transform jugadorDestino)
    {
        jugador = jugadorDestino;
        Lanzar();
    }

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator DestruirPiedra()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void Lanzar()
    {
        if (jugador == null)
        {

            return;
        }

        Vector2 direccion = (jugador.position - transform.position).normalized;
        Vector2 fuerza = new Vector2(direccion.x * fuerzaX, fuerzaY);

        rb.AddForce(fuerza, ForceMode2D.Impulse);
        StartCoroutine(DestruirPiedra());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("PerderVida", damage, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }
}

