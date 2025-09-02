using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Rendering;

public class EnemigoDisparo : Enemigo_IA
{
    private int nroBalas = 15;
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private Transform puntoDisparo;

    private int tiempoDisparo;
    //Este mira al enemigo y se pone en una posicion en la que se pondra a dispararle
    public override void Atacar()
    {
        Posicionarse();
        Shoot();
    }
    private void Shoot()
    {
        //Dispara si el tiempo de disparo es 0 y tiene balas
        if (tiempoDisparo <= 0 && nroBalas > 0)
        {
            Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
            tiempoDisparo = 20;
            nroBalas--;
        }
        else
        {
            tiempoDisparo--;
        }
    }
    private void Posicionarse()
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
        else if (distancia <= 4)
        {
            transform.position = Vector2.MoveTowards(transform.position, -jugador.position, speed * Time.deltaTime);
            bool isPlayerOnRight = jugador.position.x > transform.position.x;
            Flip(isPlayerOnRight);
        }
    }

}
