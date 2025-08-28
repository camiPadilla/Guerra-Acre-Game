using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cajasDestruibles : ObjetoDestruible
{
    [SerializeField] GameObject recogible;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (getVidas() == 0)
        {
            Debug.Log("se destruyo un objeto");
            loot();
        }
    }
    private void loot()
    {
        ObjetoRecogible recogibleDatos = Instantiate(recogible, transform.position, transform.rotation).GetComponent<ObjetoRecogible>();
        recogibleDatos.setCantidad(3);
        recogibleDatos.setNombre("hola");

    }
    private void OnDisable()
    {
        loot();
    }
}
