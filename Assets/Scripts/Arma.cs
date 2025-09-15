using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    [SerializeField] int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Reposicionar(Vector3 Npos)
    {
        transform.position = Npos;
    }
    public void Activar()
    {
        gameObject.SetActive(true);
    }
    public void Desactivar()
    {
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Destruible"))
        {
            collision.gameObject.GetComponent<ObjetoDestruible>().Damage(damage);
        }
        if (collision.transform.CompareTag("Enemigo"))
        {
            collision.gameObject.GetComponent<Enemigo_IA>().vida -= damage;
        }
    }
}
