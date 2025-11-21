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
    float distancia = Vector2.Distance(transform.position, jugador.position);

    if (distancia > stoppingDistance)
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        rbEnemigo.velocity = new Vector2(direccion.x * speed, rbEnemigo.velocity.y);
    }
    else
    {
        rbEnemigo.velocity = new Vector2(0, rbEnemigo.velocity.y);
        //Aquí puedes lanzar la animación real de ataque
    }

    Flip(jugador.position.x > transform.position.x);
}
}
