using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Proyectil : Arma
{

    AtaquePersonaje personaje;
    [SerializeField] bool enUso;
    [SerializeField] Rigidbody2D piedraRigid;
    [SerializeField] int tipo;
    // Start is called before the first frame update
    void Start()
    {
        piedraRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Instanciar(AtaquePersonaje nuevoPersonaje)
    {
        personaje = nuevoPersonaje;
    }

    public void Activar()
    {
        gameObject.SetActive(true);
        enUso = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(!collision.transform.CompareTag("Player"));
        //Debug.Log(enUso);
        if (enUso && !collision.transform.CompareTag("Player"))
        {
            enUso = false;
            gameObject.SetActive(false);
            Reposicionar(personaje.origen.position);
            personaje.GuardarEnCola(this,tipo);
            //if (collision.transform.CompareTag("Destruible"))
            //{
            //    collision.gameObject.GetComponent<ObjetoDestruible>().Da�o();
            //    //Destroy(collision.gameObject);
            //}
        }
    }
    public void Impulso(float fuerza, int dir, float dirY)
    {
        piedraRigid.AddForce(Vector2.up * dirY * fuerza + Vector2.right * fuerza * dir);
    }
}
