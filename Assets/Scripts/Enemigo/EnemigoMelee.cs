using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMelee : Enemigo_IA
{
    [SerializeField] private float stoppingDistance;
    [SerializeField] Collider2D arma;
    [SerializeField] public int damageArma;
    //Este enemigo se acerca al jugador y lo ataca con un machete
    public override void Atacar()
    {
        FollowPlayer();
        arma.enabled = true;
        //Animacion de ataque con machete

    }
    public void DesactivarArma() 
    {
        arma.enabled = false;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<SaludPersonaje>().PerderVida(this.damageArma);
            Debug.Log("le di al player");
        }
    }
}
