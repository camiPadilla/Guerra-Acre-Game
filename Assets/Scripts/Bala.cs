using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : Arma
{
    int direccion; 
    [SerializeField] float fuerzaBala;
    [SerializeField] bool enUso;
    bool Personaje;
    [SerializeField] Rigidbody2D balaRigid;
    // Start is called before the first frame update
    void Start()
    {
        balaRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x + velocidadBala * dirX * Time.deltaTime, transform.position.y,transform.position.z);
    }
    public void Impulso()
    {
        balaRigid.AddForce(new Vector3());
    }


}
