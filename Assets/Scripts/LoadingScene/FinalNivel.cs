using System.Collections;
using System.Collections.Generic;
using PantallaCarga;
using UnityEngine;

public class FinalNivel : MonoBehaviour
{

    public void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //by the way funciona asi
        //cambiar a una tacla inteactuable, para iniciar la cinematica y continuar a la pantalla de progeso y finalmente al siguiente nivel
        if (other.CompareTag("Player"))
        {
            print("holaa");
            pantallaProgreso.SetActive(true);
            //Aparece pantalla de progreso supongo
        }
    }
   
}
