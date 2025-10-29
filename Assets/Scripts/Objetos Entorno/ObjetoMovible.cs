using TarodevController;
using UnityEngine;

public class ObjetoMovible : MonoBehaviour
{
    Rigidbody2D miCuerpo;
    
    [SerializeField] LayerMask personaje;
    [SerializeField] float distanciaRaycast;

    private bool arrastrando = false; // ← añade esta variable al inicio de la clase

    // Start is called before the first frame update
    void Start()
    {
        miCuerpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per fra
   
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, distanciaRaycast, personaje) || Physics2D.Raycast(transform.position, Vector2.right, distanciaRaycast, personaje))
            {
                HUDManager.instancia.MostrarInteraccion(transform.position, GetComponent<SpriteRenderer>().bounds.extents.y, "movible");
                PlayerController controladorMovimiento = collision.gameObject.GetComponent<PlayerController>();
                if (collision.gameObject.GetComponent<InputPlayer>().GetMoviendo())
                {
                    tag = "movible";
                    HUDManager.instancia.Ocultar();
                    miCuerpo.mass = 10f;
                    Movimiento(controladorMovimiento);
                }
                else
                {
                    SoundEvents.DetenerArrastrarObjeto?.Invoke(); // Sonido by Chelo :D
                    DetenerObjeto(controladorMovimiento);


                }
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("cabeza"))
        {
         
                collision.gameObject.GetComponentInParent<SaludPersonaje>().PerderVida(6);
   
        }
    }

    private void Movimiento(PlayerController jugadorMovimiento)
    {
        float direccionX = Input.GetAxis("Horizontal");
        //Debug.Log(direccionX);
        if (direccionX < 0 && Physics2D.Raycast(transform.position, Vector2.left, distanciaRaycast, personaje))
        {
            miCuerpo.velocity = new Vector2(-3, miCuerpo.velocity.y);
            
            jugadorMovimiento.enabled = false;

            //ADDED BY CHELO :D
            if (!arrastrando)
            {
                arrastrando = true;
                SoundEvents.ArrastrarObjeto?.Invoke();
            }

        }
        if (direccionX > 0 && Physics2D.Raycast(transform.position, Vector2.right, distanciaRaycast, personaje))
        {
            miCuerpo.velocity = new Vector2(3, miCuerpo.velocity.y);
            
            jugadorMovimiento.enabled = false;
            //ADDED BY CHELO :D
            if (!arrastrando)
            {
                arrastrando = true;
                SoundEvents.ArrastrarObjeto?.Invoke();
            }
        }
        if (direccionX > 0 && Physics2D.Raycast(transform.position, Vector2.left, distanciaRaycast, personaje))
        {
            //ADDED BY CHELO :D
            jugadorMovimiento.enabled = true;
            if (arrastrando)
            {
                arrastrando = false;
                SoundEvents.DetenerArrastrarObjeto?.Invoke();
            }
        }
        if (direccionX < 0 && Physics2D.Raycast(transform.position, Vector2.right, distanciaRaycast, personaje))
        {
            //ADDED BY CHELO :D
            jugadorMovimiento.enabled = true;
            if (arrastrando)
            {
                arrastrando = false;
                SoundEvents.DetenerArrastrarObjeto?.Invoke();
            }
        }
        // ADDED FIX BY CHELO :D
        if (arrastrando)
        {
            bool contactoIzquierda = Physics2D.Raycast(transform.position, Vector2.left, distanciaRaycast, personaje);
            bool contactoDerecha = Physics2D.Raycast(transform.position, Vector2.right, distanciaRaycast, personaje);

            // Si no hay contacto o el jugador no está moviéndose horizontalmente, se detiene el sonido
            if ((!contactoIzquierda && !contactoDerecha) || direccionX == 0)
            {
                arrastrando = false;
                SoundEvents.DetenerArrastrarObjeto?.Invoke();
                jugadorMovimiento.enabled = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            HUDManager.instancia.Ocultar();
            
            DetenerObjeto(collision.gameObject.GetComponent<PlayerController>());
            
        }
    }
    private void DetenerObjeto(PlayerController jugadorMovimiento)
    {
        jugadorMovimiento.enabled = true;
        miCuerpo.mass = 100f;
        miCuerpo.velocity = Vector2.zero;
    }

}
