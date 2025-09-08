using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Enemigo_IA : MonoBehaviour
{
    //conducta de la ia
   
    [SerializeField] public Rigidbody2D rbEnemigo;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] bool patrullaje;
    [SerializeField] public float rangoVision = 6f;
    [SerializeField] public int vida = 3;
     public float speed;
    public Transform jugador;
    public List <GameObject> Armas;
    private bool isFacingRight = false;
    private int currentWayPoint = 0;
    private bool enEspera;
    //funcion atacar que sera sobreecrita por sus hijos
    public abstract void Atacar();
    
    public void Awake()
    {
         rbEnemigo = GetComponent<Rigidbody2D>();
         jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

   void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        if (patrullaje)
        {
            // si jugador en rango Y no se salió demasiado del área de patrullaje
            if (distanciaJugador < rangoVision &&
                Vector2.Distance(transform.position, wayPoints[currentWayPoint].position) < 9f)
            {
                Atacar();
            }
            else
            {
                PatrullajeIA();
            }
        }
        else
        {
            if (distanciaJugador < rangoVision)
            {
                Atacar();
            }
            else
            {
                rbEnemigo.velocity = Vector2.zero; // idle
            }
        }
    }

    //funcion para voltear al enemigo en base a la posicion del jugador
    public void Flip(bool isPlayerOnRight)
    {
        //verifica si el enemigo esta mirando a la derecha y si el jugador esta a la derecha
        if (isFacingRight && !isPlayerOnRight || !isFacingRight && isPlayerOnRight)
        {
            //cambia la direccion del enemigo
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
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
    private void RecibirDano()
    {
        //Animacion de recibir daño
    }
    //por definir
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BalaJugador"))
        {
            vida -= 2;
            RecibirDano();
            Morir();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("PiedraJugador"))
        {
            vida--;
            RecibirDano();
            Morir();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Melee"))
        {
            vida-=2;
            RecibirDano();
            Morir();
        }
    }*/
}
