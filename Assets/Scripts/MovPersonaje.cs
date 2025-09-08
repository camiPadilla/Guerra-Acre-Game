using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
    [SerializeField] float aceleracion;
    [SerializeField] float velMax;
    [SerializeField] float fuerzaSalto;
    [SerializeField] Rigidbody2D miRigid;
    bool enSuelo;
    [SerializeField]LayerMask capaSuelo;
    [SerializeField] float distanciaRayCast = 0.1f;
    [SerializeField] Transform puntoRayCast;
    [SerializeField] SpriteRenderer miSprite;
    [SerializeField] Animator miAnimator;
    float realentizador;
    int direccion;
    bool enAccion;
    // Start is called before the first frame update
    bool jalando = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            miAnimator.SetBool("agachado", true);
            realentizador = 0.8f;
        }
        else
        {
            miAnimator.SetBool("agachado", false);
            realentizador = 1;
        }
        float entradaX = Input.GetAxis("Horizontal");
        if (!enAccion && entradaX != 0 && Mathf.Abs(miRigid.velocity.x)<=velMax && !jalando)
        {
            miRigid.velocity = miRigid.velocity + Vector2.right * entradaX * aceleracion * realentizador * Time.deltaTime;
            if (miRigid.velocity.x > 0)
            {
                miSprite.flipX = true; 
                SetDireccion(1);
            }
            else
            {
                miSprite.flipX = false; 
                SetDireccion(-1);
            }
        }
        DetectarSuelo();
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            miRigid.AddForce(Vector2.up * fuerzaSalto* realentizador);
            enSuelo = false;
        }


    }
    void DetectarSuelo()
    {
        // Lanzar RayCast hacia abajo para detectar suelo
        RaycastHit2D hit = Physics2D.Raycast(puntoRayCast.position, Vector2.down, distanciaRayCast, capaSuelo);

        // Visualizar el RayCast en el editor (solo para debugging)
        Debug.DrawRay(puntoRayCast.position, Vector2.down * distanciaRayCast, Color.red);

        // Comprobar si el RayCast golpeó algo
        if (hit.collider != null)
        {
            enSuelo = true;
        }
        else
        {
            enSuelo = false;
        }
    }
    public int GetDireccion()
    {
        return direccion;
    }
    private void SetDireccion(int Ndir)
    {
        direccion = Ndir;
    }
    public void SetAccion(bool Nac)
    {
        enAccion = Nac;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("movible") && Input.GetAxis("Horizontal") !=0)
        {
            collision.gameObject.GetComponent<ObjetoMovible>().jalar(Input.GetAxis("Horizontal"));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.transform.CompareTag("Escenario"))
            {
                Color colorEscenario = collision.gameObject.GetComponent<SpriteRenderer>().color;
                StartCoroutine(Desvanecer(collision, colorEscenario, colorEscenario.a, 0.5f));
                //collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(colorEscenario.r,colorEscenario.g,colorEscenario.b, 0.5f);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Escenario"))
        {
            Color colorEscenario = collision.gameObject.GetComponent<SpriteRenderer>().color;
            StartCoroutine(Desvanecer(collision, colorEscenario, colorEscenario.a, 1f)); ;
            //collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(colorEscenario.r, colorEscenario.g, colorEscenario.b, 1);
        }
    }
    private IEnumerator Desvanecer(Collider2D collision, Color color, float inicial, float objetivo)
    {
        float diferencia = objetivo - inicial;
        while (inicial != objetivo)
        {
            color = new Color(color.r, color.g, color.b, inicial);
            inicial = inicial + diferencia / 16;
            collision.gameObject.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(2 / 16);
        }
    }
    //private IEnumerator Reaaparecer(Collider2D collision, Color color, float inicial, float objetivo)
    //{
    //    float diferencia = objetivo - inicial;
    //    while (inicial <= objetivo)
    //    {
    //        Debug.Log(inicial);
    //        color = new Color(color.r, color.g, color.b, inicial);           
    //        inicial = inicial + diferencia / 16;
    //        collision.gameObject.GetComponent<SpriteRenderer>().color = color;
    //        yield return new WaitForSeconds(2 / 16);
    //    }
    //    Debug.Log("salida");
    //}

}
