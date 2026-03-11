using TarodevController;
using UnityEngine;

public class ObjetoMovible : MonoBehaviour
{
    Rigidbody2D miCuerpo;
    private float velocidadAnteriorX = 0f;
    [SerializeField] LayerMask personaje;
    [SerializeField] float distanciaRaycast;

    private bool arrastrando = false;
    [SerializeField] private bool caja = false;
    // Start is called before the first frame update
    void Start()
    {
        miCuerpo = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float velActual = Mathf.Abs(miCuerpo.velocity.x);
        if (velocidadAnteriorX < 0.1f && velActual > 0.5f) SoundEvents.ArrastrarObjeto?.Invoke(); 
        velocidadAnteriorX = velActual;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerAnimator anim = collision.gameObject.GetComponentInChildren<PlayerAnimator>();

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
                    controladorMovimiento.Detener();
                    Movimiento(controladorMovimiento);

                    if (!arrastrando)
                    {
                        arrastrando = true;
                        anim.Arrastrar();
                        SoundEvents.ArrastrarObjeto?.Invoke();
                    } 
                }
                else if (!collision.gameObject.GetComponent<InputPlayer>().GetMoviendo())
                {
                    DetenerObjeto(controladorMovimiento);
                }
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("cabeza"))
        {
                Debug.Log("te mato una piedra");
            Debug.Log(collision.transform.name);
            if(!caja)
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
          

        }
        if (direccionX > 0 && Physics2D.Raycast(transform.position, Vector2.right, distanciaRaycast, personaje))
        {
            miCuerpo.velocity = new Vector2(3, miCuerpo.velocity.y);
            
            jugadorMovimiento.enabled = false;
           
        }
        if (direccionX > 0 && Physics2D.Raycast(transform.position, Vector2.left, distanciaRaycast, personaje))
        {
            //ADDED BY CHELO :D
            jugadorMovimiento.enabled = true;
          
        }
        if (direccionX < 0 && Physics2D.Raycast(transform.position, Vector2.right, distanciaRaycast, personaje))
        {
            //ADDED BY CHELO :D
            jugadorMovimiento.enabled = true;
           
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
        var anim = jugadorMovimiento.GetComponentInChildren<PlayerAnimator>();
        if (anim != null) anim.DetenerArrastre();

        arrastrando = false;
        jugadorMovimiento.enabled = true;
        miCuerpo.mass = 50f;
        miCuerpo.velocity = Vector2.zero;
        SoundEvents.DetenerArrastrarObjeto?.Invoke(); // Sonido by Chelo :D
    }

}
