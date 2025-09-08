using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo_IA : MonoBehaviour
{
    //conducta de la ia
   
    [SerializeField] public Rigidbody2D rbEnemigo;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] bool patrullaje;
    [SerializeField] public float rangoVision = 6f;
     public float speed;
    public Transform jugador;
    public List <GameObject> Armas;
    private bool isFacingRight = false;
    private int currentWayPoint = 0;
    private bool enEspera;
    //funcion atacar que sera sobreecrita por sus hijos
    public abstract void Atacar();
    


    void Update()
    {
        //Verificamos si el enemigo es del tipo patrullaje o si su estado es de patrullaje
        if (!patrullaje)
        {
            //Animacion idle
            //Verifica la posicion del jugador para atacar al enemigo al enemigo
            if (Vector2.Distance(transform.position, jugador.position) < 4)
            {
                Atacar();
            }
        }

        else if (patrullaje)
        {
            //verifica la distancia entre el enemigo y el jugador
            if (Vector2.Distance(transform.position, jugador.position) < 4)
            {
                Atacar();
                if (Vector2.Distance(transform.position, wayPoints[currentWayPoint].position) > 9f)
                {
                    PatrullajeIA();
                }
            }
            else
            {
                PatrullajeIA();
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

        Vector3 destino = new Vector3(wayPoints[currentWayPoint].position.x,transform.position.y,transform.position.z);

        if (transform.position.x != destino.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, destino, speed * Time.deltaTime);
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
        //Animacion de muerte
    }
    private void RecibirDano()
    {
        //Animacion de recibir daño
    }
    
}
