using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo_IA : MonoBehaviour
{
    //conducta de la ia
    public float speed;

    public Transform jugador;
    private bool isFacingRight = false;
    //puntos de patrullaje
    [SerializeField] private Transform[] wayPoints;
    //lista de armas del enemigo
    private int currentWayPoint = 0;
    private bool isWaiting;
    //funcion atacar que sera sobreecrita por sus hijos
    public abstract void Atacar();
    void Update()
    {
        //verifica la distancia entre el enemigo y el jugador
        if (Vector2.Distance(transform.position, jugador.position) < 4)
        {
            Atacar();
            //sigue al jugador si este se aleja mas de 4 unidades vuelve a patrullar
            if (Vector2.Distance(transform.position, wayPoints[currentWayPoint].position) > 4)
            {
                PatrullajeIA();
            }
        }
        else
        {
            //inicia el patrullaje
            PatrullajeIA();
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
    private void PatrullajeIA()
    {
        //mueve al enemigo entre los waypoints
        if (transform.position != wayPoints[currentWayPoint].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPoint].position, speed * Time.deltaTime);
        }
        else if (!isWaiting)
        {
            //inicia corutina para esperar en el waypoint
            StartCoroutine(WaitAtWayPoint());
        }
    }
    //corutina para esperar en el waypoint
    IEnumerator WaitAtWayPoint()
    {
        //si esta esperando no puede moverse
        isWaiting = true;
        yield return new WaitForSeconds(2f);
        //cambia al siguiente waypoint
        currentWayPoint++;
        if (currentWayPoint >= wayPoints.Length)
        {
            //si llega al final de los waypoints vuelve al primero
            currentWayPoint = 0;
        }
        //al llegar a un waypoint espera 2 segundos y voltea
        isWaiting = false;
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
