using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class ObjetoMovible : MonoBehaviour
{
    Rigidbody2D miCuerpo;
    bool movible = false;
    
    // Start is called before the first frame update
    void Start()
    {
        miCuerpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movible == false)
        {
            miCuerpo.mass = 10;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && collision.gameObject.GetComponent<InputPlayer>().getInteractuable())
        {
            Debug.Log("se puede iniciar el enlace");
            mover();
        }
        else
        {
            movible = false;
            this.tag = "Untagged";
        }
    }
    void mover()
    {
        this.tag = "movible";
        movible = true;
        miCuerpo.mass = 0.5f;
    }
    public void jalar(float direccion)
    {
        Debug.Log("esta sienod jalado");
        if (direccion < 0)
        {
            miCuerpo.AddForce(Vector2.left * 8f, ForceMode2D.Force);
        }
        else
        {
            miCuerpo.AddForce(Vector2.right * 8f, ForceMode2D.Force);
        }
        
    }
}
