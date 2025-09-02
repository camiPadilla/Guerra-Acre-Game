using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPiedra : Enemigo_IA
{
    [SerializeField] private GameObject piedra;
    [SerializeField] private Transform lanzarDesde;
    private int tiempoLanzamiento;
    private int nroPiedras = 10;
    
    //Al igual que disparo este se para en una posicion y le lanza piedra
    public override void Atacar()
    {


    }
    public void Posicionarse()
    {
         //Enemigo se posiciona
        float distancia = Vector2.Distance(transform.position, jugador.position);
        if (distancia < 10)
        {
            transform.position = Vector2.MoveTowards(transform.position, jugador.position, speed * Time.deltaTime);
            bool isPlayerOnRight = jugador.position.x > transform.position.x;
            Flip(isPlayerOnRight);
        }
        //enemigo toma distancia para atacar
        else if (distancia <= 7)
        {
            transform.position = Vector2.MoveTowards(transform.position, -jugador.position, speed * Time.deltaTime);
            bool isPlayerOnRight = jugador.position.x > transform.position.x;
            Flip(isPlayerOnRight);
        }
    }
    private void Lanzar()
    {
        if (nroPiedras > 7)
        {
            Instantiate(piedra, transform.position, transform.rotation);
            tiempoLanzamiento = 20;
            nroPiedras--;
        }
        else
        {
            tiempoLanzamiento--;
        }
    }
}
