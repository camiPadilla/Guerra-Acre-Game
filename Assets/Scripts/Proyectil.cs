using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Proyectil : Arma
{
    Vector2 posicionGuardada;
    float fuerzaGuardada;
    int dirGuardado;
    float dirYGuardado;
    float tiempoEspera = 0;
    AtaquePersonaje personaje;
    [SerializeField] bool enUso;
    [SerializeField] Rigidbody2D piedraRigid;
    [SerializeField] int tipo;
    bool falso = false;
    // Start is called before the first frame update
    void Start()
    {
        
        piedraRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    
    public void Instanciar(AtaquePersonaje nuevoPersonaje)
    {
        personaje = nuevoPersonaje;
    }

    public void ActivarProyectil()
    {
        gameObject.SetActive(true);
        enUso = true;
        StartCoroutine(TiempoVuelta());
    }
    public void DesactivarProyectil()
    {
        enUso = false;
        gameObject.SetActive(false);
        Reposicionar(personaje.origen.position);
        personaje.GuardarEnCola(this, tipo);
    }
    public IEnumerator TiempoVuelta()
    {
        Debug.Log("Salgo");
        yield return new WaitForSeconds(3);
        DesactivarProyectil();
        Debug.Log("Vuelvo");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log(!collision.transform.CompareTag("Player"));
        //Debug.Log(enUso);
        if (enUso && !collision.transform.CompareTag("Player"))
        {
            if(!CompareTag("falso"))
            DesactivarProyectil();
            //if (collision.transform.CompareTag("Destruible"))
            //{
            //    collision.gameObject.GetComponent<ObjetoDestruible>().Daño();
            //    //Destroy(collision.gameObject);
            //}
        }
    }
    public void ResetVelocidad()
    {
        piedraRigid.velocity = Vector2.zero;
    }
    public void Impulso(float fuerza, int dir, float dirY)
    {

        posicionGuardada = transform.position;
        fuerzaGuardada = fuerza;
        dirGuardado = dir;
        dirYGuardado = dirY;
        piedraRigid.AddForce(Vector2.up * dirY * fuerza + Vector2.right * fuerza * dir);
        if (transform.CompareTag("falso"))
        {
            falso = true;
        }
    }
    void Update()
    {
        if (falso)
        {
            //Debug.Log("hola");
            tiempoEspera += Time.deltaTime;

            if (falso && tiempoEspera >= 0.01f)
            {
                tiempoEspera = 0;
                piedraRigid.velocity = Vector2.zero;
                transform.position = posicionGuardada;
                piedraRigid.AddForce(Vector2.up * dirYGuardado * fuerzaGuardada + Vector2.right * fuerzaGuardada * dirGuardado);


            }
        }
    }
}
