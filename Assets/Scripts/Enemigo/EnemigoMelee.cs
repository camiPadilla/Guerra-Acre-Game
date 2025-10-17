using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMelee : Enemigo_IA
{
    [SerializeField] private float stoppingDistance;
    [SerializeField] GameObject arma;
    [SerializeField] public int damageArma;
    //Este enemigo se acerca al jugador y lo ataca con un machete
    public override void Atacar()
    {
        FollowPlayer();
        arma.SetActive(true);
        //Animacion de ataque con machete

    }
    //cambiar a fisicas 
    private void FollowPlayer()
    {
        //mueve al enemigo hacia el jugador calculando la distancia
        if (Vector2.Distance(transform.position, jugador.position) > stoppingDistance)
        {
            rbEnemigo.velocity = new Vector2((jugador.position.x - transform.position.x) * speed, rbEnemigo.velocity.y);
            print("Persiguiendo");
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
