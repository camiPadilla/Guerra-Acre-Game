using System.Collections;
using UnityEngine;
using TarodevController;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocidadBala = 10f;
    [SerializeField] public int damage = 2;

    private Transform jugador;

    public void Inicializar(Transform jugadorDestino)
    {
        jugador = jugadorDestino;
        Disparar();
    }

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void Disparar()
    {
        if (jugador == null)
        {
            Debug.LogWarning("⚠️ [BalaEnemigo] No se asignó jugador al inicializar.");
            return;
        }

        Vector2 direccion = (jugador.position - transform.position).normalized;
        rb.velocity = direccion * velocidadBala;
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
}
