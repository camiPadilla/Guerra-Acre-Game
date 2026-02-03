using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : Arma
{
    int direccion; 
    [SerializeField] float fuerzaBala;
    [SerializeField] bool enUso;
    [SerializeField] GameObject dire;
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
        
        //balaRigid.velocity = dir * fuerzaBala* Time.deltaTime;
        float angulo = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }


}
