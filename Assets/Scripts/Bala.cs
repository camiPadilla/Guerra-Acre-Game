using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    int direccion; 
    [SerializeField] float velocidadBala;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + velocidadBala * direccion * Time.deltaTime, transform.position.y,transform.position.z);
    }

}
