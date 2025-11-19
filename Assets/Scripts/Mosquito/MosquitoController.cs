using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// Controlador del enemigo "Mosquito".
/// Comportamiento:
/// - Patrulla alrededor de su posición inicial moviéndose a posiciones aleatorias.
/// - Con una probabilidad configurada, detecta al jugador (si está dentro de rango) y ataca moviéndose hacia él.
/// - Al colisionar con el jugador aplica daño; al colisionar con una hitbox recibe daño.
/// </summary>
public class MosquitoController : MonoBehaviour
{
    /// <summary>
    /// Transform del jugador. Se puede asignar desde el inspector; si no se asigna se busca por tag "Player" en Start().
    /// </summary>
    [SerializeField] Transform PosicionJugador;

    /// <summary>
    /// Indica si el mosquito está patrullando (true) o atacando al jugador (false).
    /// </summary>
    bool patrullando = true;

    /// <summary>
    /// Valor entero utilizado para calcular la probabilidad de iniciar el ataque.
    /// Random.Range(0, probabilidad) == 0 => se considera la condición de ataque.
    /// Valores más altos reducen la frecuencia de ataque.
    /// </summary>
    [SerializeField] int probabilidad;

    /// <summary>
    /// Velocidad de movimiento del mosquito (unidades por segundo).
    /// </summary>
    [SerializeField] int velocidad;

    /// <summary>
    /// Cantidad de daño que inflige al jugador al colisionar.
    /// </summary>
    [SerializeField] int damage;

    /// <summary>
    /// Posición objetivo aleatoria durante la patrulla.
    /// </summary>
    Vector2 posicionAleatoria;

    /// <summary>
    /// Posición inicial donde se genera el mosquito; sirve como centro de su área de patrulla.
    /// </summary>
    Vector2 posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        // Guardamos la posición inicial para generar posiciones de patrulla relativas a esta.
        posicionInicial = transform.position;

        // Genera la primera posición aleatoria de patrulla.
        cambiarPosicion();

        // Si no se asignó manualmente el Transform del jugador, se busca por tag "Player".
        if (PosicionJugador == null)
        {
            PosicionJugador = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Decide si iniciar o no el ataque según probabilidad y distancia.
        picar();

        if (!patrullando)
        {
            // Si no patrulla, se mueve hacia la posición del jugador a la velocidad indicada.
            transform.position = Vector2.MoveTowards(transform.position, PosicionJugador.position, velocidad * Time.deltaTime);
        }
        else
        {
            // Si patrulla, se mueve hacia una posición aleatoria.
            // Si está suficientemente cerca de esa posición, genera otra.
            if (Vector2.Distance(posicionAleatoria, transform.position) > 0.7f)
            {
                transform.position = Vector2.MoveTowards(transform.position, posicionAleatoria, velocidad * Time.deltaTime);
            }
            else
            {
                cambiarPosicion();
            }
        }
    }

    /// <summary>
    /// Decide si el mosquito inicia un ataque hacia el jugador.
    /// - Calcula un número aleatorio en [0, probabilidad)
    /// - Si el número es 0 y el jugador está dentro de 5 unidades, cambia de estado a atacar.
    /// - En caso contrario vuelve a patrullar.
    /// </summary>
    void picar()
    {
        int numeroRandom = Random.Range(0, probabilidad);
        //Debug.Log(numeroRandom);

        // Solo ataca si el número aleatorio es 0 y el jugador está suficientemente cerca.
        if (numeroRandom == 0 && Vector2.Distance(transform.position, PosicionJugador.position) < 5f)
        {
            patrullando = false;
            Debug.Log("listo para atacar");
            // Orienta el sprite hacia el jugador antes de moverse.
            CambiarDireccion(PosicionJugador.position.x);
        }
        else
        {
            // Si no se cumplen las condiciones, permanece patrullando.
            patrullando = true;
        }
    }

    /// <summary>
    /// Cambia la escala en X del transform para "mirar" hacia el objetivo según su posición X.
    /// Escala a (1,1,1) si el objetivo está a la izquierda; (-1,1,1) si está a la derecha.
    /// </summary>
    /// <param name="xobjetivo">Coordenada X del objetivo hacia el que se quiere orientar.</param>
    void CambiarDireccion(float xobjetivo)
    {
        if (transform.position.x > xobjetivo)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    /// <summary>
    /// Maneja colisiones con trigger 2D:
    /// - Si colisiona con el jugador ("Player"): vuelve a patrullar y envía el mensaje "PerderVida" con el daño.
    /// - Si colisiona con una hitbox ("hitbox"): envía el mensaje "perderVida" hacia sí mismo (indica recibir daño).
    /// </summary>
    /// <param name="collision">Collider con el que colisiona.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // Al golpear al jugador, resetear a modo patrulla y aplicar daño.
            patrullando = true;
            collision.gameObject.SendMessageUpwards("PerderVida", damage);
        }
        if (collision.transform.CompareTag("hitbox"))
        {
            // Si recibe impacto (por ejemplo un ataque del jugador), notificar que perdió vida.
            Debug.Log("me toco algo que miedo");
            gameObject.SendMessage("perderVida");
        }
    }

    /// <summary>
    /// Genera una nueva posición aleatoria dentro de un rango alrededor de la posición inicial
    /// y oriente el sprite hacia esa posición.
    /// </summary>
    void cambiarPosicion()
    {
        posicionAleatoria = new Vector2(
            posicionInicial.x + Random.Range(-3f, 3f),
            posicionInicial.y + Random.Range(-2f, 1f)
        );

        // Orienta hacia la nueva posición objetivo para que la animación/mirada sea coherente.
        CambiarDireccion(posicionAleatoria.x);
    }
}