using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField]private float velocidadBala;
    private float direccion;
    public void Update()
    {
        transform.position = new Vector3(transform.position.x + velocidadBala * Time.deltaTime * direccion, 
            transform.position.y,
            transform.position.z);
    }
    public void DireccionBala(float drecEn)
    {
        direccion = Mathf.Sign(drecEn);
    }
}
