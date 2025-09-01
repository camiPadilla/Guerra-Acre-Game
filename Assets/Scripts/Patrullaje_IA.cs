using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullaje_IA : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    private int currentWayPoint = 0;
     [SerializeField] private int speed;
    private bool isWaiting;
    // Update is called once per frame
    void Update()
    {
        if (transform.position != wayPoints[currentWayPoint].position)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, wayPoints[currentWayPoint].position, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(newPosition);
        }
        else if (!isWaiting)
        {
            StartCoroutine(WaitAtWayPoint());
        }
    }
    IEnumerator WaitAtWayPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2f);
        currentWayPoint++;
        if (currentWayPoint >= wayPoints.Length)
        {
            currentWayPoint = 0;
        }
        isWaiting = false;
        Flip();
    }
    private void Flip()
    {
        if(transform.position.x < wayPoints[currentWayPoint].position.x && !isWaiting)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
        else if(transform.position.x  > wayPoints[currentWayPoint].position.x && !isWaiting)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
    }
}
