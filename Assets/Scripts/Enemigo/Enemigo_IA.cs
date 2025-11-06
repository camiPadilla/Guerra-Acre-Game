using System.Collections;
using UnityEngine;

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
    [Header("Base IA")]
    [SerializeField] public Rigidbody2D rbEnemigo;
    [SerializeField] protected Transform[] wayPoints;
    [SerializeField] private bool patrullaje;
    [SerializeField] public float rangoVision = 10f;
    [SerializeField] public int vida = 3;
    [SerializeField] public float speed = 2f;

    [Header("Restricciones")]
    [Tooltip("Distancia máxima que puede alejarse del waypoint actual antes de volver")]
    [SerializeField] protected float maxRoamDistance = 6f;

    public estadosEnemigo estadoActual;
    public Transform jugador;
    protected bool isFacingRight = false;
    public int currentWayPoint = 0;
    private bool enEspera;

    public abstract void Atacar();

    private void Awake()
    {
        rbEnemigo = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        estadoActual = patrullaje ? estadosEnemigo.patrullaje : estadosEnemigo.idle;
    }

    private void Update()
    {
        if (jugador == null)
            return;

        Mover();
    }

    protected virtual void Mover()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        float distanciaAlWP = (wayPoints != null && wayPoints.Length > 0)
            ? Vector2.Distance(transform.position, wayPoints[currentWayPoint].position)
            : 0f;

        if (distanciaAlWP > maxRoamDistance)
        {
            currentWayPoint = FindClosestWaypointIndex();
            estadoActual = estadosEnemigo.patrullaje;
        }

        switch (estadoActual)
        {
            case estadosEnemigo.idle:
                rbEnemigo.velocity = Vector2.zero;
                if (distanciaJugador < rangoVision)
                    estadoActual = estadosEnemigo.ataque;
                else if (patrullaje)
                    estadoActual = estadosEnemigo.patrullaje;
                break;

            case estadosEnemigo.patrullaje:
                PatrullajeIA();
                if (distanciaJugador < rangoVision)
                    estadoActual = estadosEnemigo.ataque;
                break;

            case estadosEnemigo.ataque:
                Atacar();
                break;

            case estadosEnemigo.muerto:
                rbEnemigo.velocity = Vector2.zero;
                break;
        }
    }

    public void Flip(bool mirarDerecha)
    {
        if (mirarDerecha == isFacingRight) return;
        isFacingRight = mirarDerecha;
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * (isFacingRight ? 1f : -1f);
        transform.localScale = escala;
    }

    protected void PatrullajeIA()
    {
        if (wayPoints == null || wayPoints.Length == 0) return;

        Vector2 destino = new Vector2(wayPoints[currentWayPoint].position.x, transform.position.y);

        if (Mathf.Abs(transform.position.x - destino.x) > 0.1f && !enEspera)
        {
            float direccion = Mathf.Sign(destino.x - transform.position.x);
            rbEnemigo.velocity = new Vector2(direccion * speed, rbEnemigo.velocity.y);
        }
        else if (!enEspera)
        {
            StartCoroutine(WaitAtWayPoint());
        }
    }

    IEnumerator WaitAtWayPoint()
    {
        enEspera = true;
        rbEnemigo.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        currentWayPoint++;
        if (currentWayPoint >= wayPoints.Length) currentWayPoint = 0;
        enEspera = false;
        FlipPoint();
    }

    private void FlipPoint()
    {
        if (wayPoints == null || wayPoints.Length == 0) return;

        if (transform.position.x > wayPoints[currentWayPoint].position.x)
        {
            SetFacing(false);
        }
        else
        {
            SetFacing(true);
        }
    }

    protected void SetFacing(bool faceRight)
    {
        isFacingRight = faceRight;
        Vector3 local = transform.localScale;
        local.x = Mathf.Abs(local.x) * (faceRight ? 1f : -1f);
        transform.localScale = local;
    }

    private int FindClosestWaypointIndex()
    {
        if (wayPoints == null || wayPoints.Length == 0) return 0;
        int best = 0;
        float bestDist = float.MaxValue;
        for (int i = 0; i < wayPoints.Length; i++)
        {
            float d = Vector2.Distance(transform.position, wayPoints[i].position);
            if (d < bestDist)
            {
                bestDist = d;
                best = i;
            }
        }
        return best;
    }

    public void RecibirDano(int damage)
    {
        vida -= damage;
        if (vida <= 0) Morir();
    }

    private void Morir()
    {
        estadoActual = estadosEnemigo.muerto;
        rbEnemigo.velocity = Vector2.zero;
        Destroy(gameObject);
    }
}