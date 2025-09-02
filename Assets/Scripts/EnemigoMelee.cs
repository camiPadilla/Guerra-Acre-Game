using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMelee : Enemigo_IA
{
    
    [SerializeField] private float stoppingDistance;
    //Este enemigo se acerca al jugador y lo ataca con un machete
    public override void Atacar()
    {
        FollowPlayer();
        //Animacion de ataque con machete

    }   
    private void FollowPlayer()
    {
        //mueve al enemigo hacia el jugador calculando la distancia
        if (Vector2.Distance(transform.position, jugador.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, jugador.position, speed * Time.deltaTime);
        }
        else
        {
            print("Detenido");
        }
        //manda que el eneimgo esta mirando al jugador para voltear
        bool isPlayerOnRight = jugador.position.x > transform.position.x;
        Flip(isPlayerOnRight);
    }
}
