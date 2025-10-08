using System.Collections.Generic;
using UnityEngine;

public class TrayectoriaPiedra : MonoBehaviour
{
    public Vector2 initialVelocity;
    public GameObject nuevoProyectil;
    public static TrayectoriaPiedra instancia;
    
    [SerializeField] GameObject proyectil;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    void Start()
    {
        //nuevoProyectil = Instantiate(proyectil, setPosicionInicial, )
    }
    private void Update()
    {
        //DrawTrajectory();
    }
    /*public void DrawTrajectory()
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < steps; i++)
        {
            float t = i * timeStep;
            
            float x = posicionInicial.x + initialVelocity.x * t;
            float y = posicionInicial.y + initialVelocity.y * t + 0.5f * gravity * t * t;

            points.Add(new Vector3(x, y, 0));
        }

        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }
    
    public void ghostcollider(Vector2 velocity, float fuerza)
    {
        
        ghost.AddForce(Vector2.up * velocity.y * fuerza + Vector2.right * velocity.x * fuerza);
        initialVelocity = ghost.totalForce;
        
    }
    public void setPosicionInicial(Vector2 posicion)
    {
        posicionInicial = posicion;
    } */
}

