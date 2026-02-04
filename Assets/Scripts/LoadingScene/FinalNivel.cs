using PantallaCarga;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class FinalNivel : MonoBehaviour
{
    [SerializeField] private GameObject pantallaProgreso;
    public bool finalizado = false;
    [SerializeField] ControladorEscena contEs;
    [SerializeField] PlayerController player;
    public void Start()
    {
        if (player == null) { 
            player = FindAnyObjectByType<PlayerController>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //by the way funciona asi
        //cambiar a una tacla inteactuable, para iniciar la cinematica y continuar a la pantalla de progeso y finalmente al siguiente nivel
        if (other.CompareTag("Player"))
        {
            print("holaa");
            finalizado = true;
            player.Detener();
            contEs.CalcularScoreFinal();
            pantallaProgreso.SetActive(true);
            GameManager.instancia.CambiarDeEstado(5);
            //Aparece pantalla de progreso supongo
            SoundEvents.DetenerPasosPasto.Invoke(); //Sonido by Chelo :D
            SoundEvents.DetenerMusica.Invoke(); //By Chelo
            SoundEvents.PlayVictory.Invoke(); //Sound by Chelo
        }
    }
   
}
