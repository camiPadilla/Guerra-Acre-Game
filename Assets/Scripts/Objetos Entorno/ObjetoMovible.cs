using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
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
                HUDManager.instancia.MostrarInteraccion(transform.position, GetComponent<SpriteRenderer>().bounds.extents.y, "movible");
            if (collision.gameObject.GetComponent<InputPlayer>().GetMoviendo())
            {
                tag = "movible";
                HUDManager.instancia.Ocultar();
                miCuerpo.mass = 0.5f;
                Movimiento();
            }
            else
            {
                tag = "Untagged";
                miCuerpo.mass = 10f;
            }
        }

    }
    private void Movimiento()
    {
        float direccionX = Input.GetAxis("Horizontal");
        //Debug.Log(direccionX);
        if (direccionX < 0 && Physics2D.Raycast(transform.position, Vector2.left, distanciaRaycast, personaje))
        {
            miCuerpo.velocity = new Vector2(-2, miCuerpo.velocity.y);
            movible = false;
        }
        if (direccionX > 0 && Physics2D.Raycast(transform.position, Vector2.right, distanciaRaycast, personaje))
        {
            miCuerpo.velocity = new Vector2(2, miCuerpo.velocity.y);
            movible = false;
        }
        if (direccionX > 0 && Physics2D.Raycast(transform.position, Vector2.left, distanciaRaycast, personaje))
        {
            movible = true;
        }
        if (direccionX < 0 && Physics2D.Raycast(transform.position, Vector2.right, distanciaRaycast, personaje))
        {
            movible = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            HUDManager.instancia.Ocultar();
            movible = false;
        }
    }


}
