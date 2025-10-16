using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;


public enum estadosEnemigo
{
    idle,
    patrullaje,
    ataque,
    muerto,
    persiguiendo
}
public abstract class Enemigo_IA : MonoBehaviour
{
    //conducta de la ia

    [SerializeField] public Rigidbody2D rbEnemigo;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] bool patrullaje;
    [SerializeField] public float rangoVision;
    [SerializeField] public int vida = 3;
    [SerializeField] private float disWy;
    //declarar enum
    public estadosEnemigo estadoActual;
    public float speed;
    public Transform jugador;
    private bool isFacingRight = false;
    public int currentWayPoint = 0;
    private bool enEspera;

    //funcion atacar que sera sobreecrita por sus hijos
    public abstract void Atacar();
    //public abstract void Mover();

    public void Awake()
    {
        rbEnemigo = GetComponent<Rigidbody2D>();

    }
    public void Start()
    {
        estadoActual = patrullaje ? estadosEnemigo.patrullaje : estadosEnemigo.idle;
    }
    void Update()
    {
        Mover();
    }
    public void Mover()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        bool jugadorDerecha = jugador.position.x > transform.position.x;
        disWy = Vector2.Distance(wayPoints[currentWayPoint].position, transform.position);
        switch (estadoActual)
        {
            case estadosEnemigo.idle:
                rbEnemigo.velocity = Vector2.zero;

                if (distanciaJugador < rangoVision)
                {
                    estadoActual = estadosEnemigo.ataque;
                }
                break;

            case estadosEnemigo.patrullaje:
                PatrullajeIA();
                if (distanciaJugador < rangoVision)
                {
                    Flip(jugadorDerecha);
                    estadoActual = estadosEnemigo.ataque;
                    
                }
                if (disWy < distanciaJugador && patrullaje)
                {
                    estadoActual = estadosEnemigo.patrullaje;
                }
                break;

            case estadosEnemigo.ataque:
                if (disWy > 5.6f)
                {
                    if(patrullaje) estadoActual = estadosEnemigo.patrullaje;
                    else estadoActual = estadosEnemigo.idle;
                }
                else
                {
                    Atacar();
                }
            break;

        case estadosEnemigo.muerto:
            rbEnemigo.velocity = Vector2.zero;
            break;
    }
}


    //funcion para voltear al enemigo en base a la posicion del jugador
    public void Flip(bool isPlayerOnRight)
    {
        if (isPlayerOnRight && !isFacingRight)
        {
            isFacingRight = true;
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
        else if (!isPlayerOnRight && isFacingRight)
        {
            isFacingRight = false;
            Vector3 localScale = transform.localScale;
            localScale.x = -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
    }

    //funcion para el patrullaje del enemigo en base a los waypoints
    public void PatrullajeIA()
    {
        if (wayPoints.Length == 0) return;

        Vector2 destino = new Vector2(wayPoints[currentWayPoint].position.x, transform.position.y);

        if (Mathf.Abs(transform.position.x - destino.x) > 0.1f && !enEspera)
        {
            //mueve al enemigo hacia el waypoint utlizando velocity y el Mathf.Sign para determinar la direccion
            float direccion = Mathf.Sign(destino.x - transform.position.x);
            rbEnemigo.velocity = new Vector2(direccion * speed, rbEnemigo.velocity.y);
        }
        else if (!enEspera)
        {
            StartCoroutine(WaitAtWayPoint());
        }
    }


    //corutina para esperar en el waypoint
    IEnumerator WaitAtWayPoint()
    {
        //si esta esperando no puede moverse
        enEspera = true;
        rbEnemigo.velocity = Vector2.zero;
        yield return new WaitForSeconds(3f);
        //cambia al siguiente waypoint
        currentWayPoint++;
        if (currentWayPoint >= wayPoints.Length)
        {
            //si llega al final de los waypoints vuelve al primero
            currentWayPoint = 0;
        }
        //al llegar a un waypoint espera 2 segundos y voltea
        enEspera = false;
        FlipPoint();
    }

    //funcion para voltear al enemigo en base a la posicion del waypoint
    private void FlipPoint()
    {
        if (transform.position.x > wayPoints[currentWayPoint].position.x)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
        else if (transform.position.x < wayPoints[currentWayPoint].position.x)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
    }

    private void Morir()
    {
        if (vida <= 0)
        {
            //Animacion de muerte
            Destroy(gameObject);
        }
    }
    public void RecibirDano(int damage)
    {
        vida -= damage;
        Morir();
    }
    //por definir y llamar 
}
