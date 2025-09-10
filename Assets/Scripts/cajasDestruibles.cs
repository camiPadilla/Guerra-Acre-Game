using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cajasDestruibles : ObjetoDestruible
{
    [SerializeField] GameObject recogible;
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    public void loot()
    {
        ObjetoRecogible recogibleDatos = Instantiate(recogible, transform.position, transform.rotation).GetComponent<ObjetoRecogible>();
        //recogibleDatos.setCantidad(3);
        recogibleDatos.setNombre("hola");

    }
    private void OnDisable()
    {        
        //Debug.Log("hola");
        //loot();
    }

}
