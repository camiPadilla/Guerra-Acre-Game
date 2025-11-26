using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luciernagaControlller : MonoBehaviour
{
    
    [SerializeField] int velocidad;
    Vector2 posicionAleatoria;
    Vector2 posicionInicial;
    


    // Start is called before the first frame update
    void Start()
    {
        //vueloMosquito = GetComponent<StudioEventEmitter>();
        posicionInicial = transform.position;
        cambiarPosicion();
        

    }

    // Update is called once per frame
    void Update()
    {
        
            
        
            if (Vector2.Distance(posicionAleatoria, transform.position) > 0.7f)
            {
                transform.position = Vector2.MoveTowards(transform.position, posicionAleatoria, velocidad * Time.deltaTime);
            }
            else
            {
                cambiarPosicion();
            }
        
    }

    
    void CambiarDireccion(float xobjetivo)
    {
        if (transform.position.x > xobjetivo)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    void cambiarPosicion()
    {
        posicionAleatoria = new Vector2(posicionInicial.x + Random.Range(-2f, 2f), posicionInicial.y + Random.Range(-1f, 1f));
        CambiarDireccion(posicionAleatoria.x);
    }
}
