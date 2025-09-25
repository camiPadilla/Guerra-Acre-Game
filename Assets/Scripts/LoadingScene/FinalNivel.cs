using System.Collections;
using System.Collections.Generic;
using PantallaCarga;
using UnityEngine;

public class FinalNivel : MonoBehaviour
{
    [SerializeField] private GameObject pantallaProgreso;
    public void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("holaa");
            pantallaProgreso.SetActive(true);
            //Aparece pantalla de progreso supongo
        }
    }
   
}
