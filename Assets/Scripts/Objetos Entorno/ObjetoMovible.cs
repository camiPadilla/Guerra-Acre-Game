using TarodevController;
using UnityEngine;

public class ObjetoMovible : MonoBehaviour
{
    Rigidbody2D miCuerpo;
    bool movible = false;
    [SerializeField] LayerMask personaje;
    [SerializeField] float distanciaRaycast;
    // Start is called before the first frame update
    void Start()
    {
        miCuerpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per fra
    public bool getMovible()
    {
        return movible;
    }
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
                    //MoverRoca(collision.gameObject);
                    Movimiento(controladorMovimiento);
                }
                else
                {
                    DetenerObjeto(controladorMovimiento);


                }
            }
        }

    }
    private void Movimiento(PlayerController jugadorMovimiento)
    {
        float direccionX = Input.GetAxis("Horizontal");
        //Debug.Log(direccionX);
        if (direccionX < 0 && Physics2D.Raycast(transform.position, Vector2.left, distanciaRaycast, personaje))
        {
            miCuerpo.velocity = new Vector2(-2, miCuerpo.velocity.y);
            movible = false;
            jugadorMovimiento.enabled = false;

        }
        if (direccionX > 0 && Physics2D.Raycast(transform.position, Vector2.right, distanciaRaycast, personaje))
        {
            miCuerpo.velocity = new Vector2(2, miCuerpo.velocity.y);
            movible = false;
            jugadorMovimiento.enabled = false;
        }
        if (direccionX > 0 && Physics2D.Raycast(transform.position, Vector2.left, distanciaRaycast, personaje))
        {
            movible = true;
            jugadorMovimiento.enabled = true;
        }
        if (direccionX < 0 && Physics2D.Raycast(transform.position, Vector2.right, distanciaRaycast, personaje))
        {
            movible = true;
            jugadorMovimiento.enabled = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            HUDManager.instancia.Ocultar();
            movible = false;
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
