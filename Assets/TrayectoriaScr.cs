using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayectoriaScr : MonoBehaviour
{
    Rigidbody2D rb; // Declarar una variable Rigidbody2D
    LineRenderer lineRenderer; // Declarar una variable LineRenderer
    public float TiempoDePrediccion = 3; //tiempo de duración de la predicción en segundos
                                         //(dibujará solo la curva que recorrerá en esta cantidad de segundos)
    public int resolution = 11; // Cantidad de segmentos de la curva
                                // (más segmentos consumirán más recursos, pero dibujarán una curva más suave)
    public LayerMask collisionMask; // Layer que detectará como obstáculo (Modificable desde el inspector)

    Vector2 finalPosition; //punto que se usará para establecer cada vértice de la curva
    Vector2 finalPositionAnt;//punto que se usará para el segmento final de la curva

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D del objeto
        lineRenderer = GetComponent<LineRenderer>(); // Obtener el componente LineRenderer del objeto
        lineRenderer.positionCount = resolution; // Establecer el número de puntos del LineRenderer a la resolución
                                                 // especificada
    }
    private void OnDisable()
    {
        lineRenderer.enabled = false; // Desactivar el LineRenderer cuando el objeto esté desactivado
    }
    private void OnEnable()
    {
        //lineRenderer.enabled = true; // Activar el LineRenderer cuando el objeto esté activado
    }
    private void FixedUpdate()
    {
        // Mueve el cuadradito con las teclas W A S D, o las flechas (pueden eliminar esta línea sin problemas,
        // no es necesaria para el cálculo y dibujado de trayectoria, solo para aplicar fuerzas al objeto)
        //rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), ForceMode2D.Impulse);

        Vector2 initialVelocity = rb.velocity; // Guardar la velocidad inicial del Rigidbody2D
        Vector2 initialPosition = rb.position; // Guardar la posición inicial del Rigidbody2D
        float timeStep = TiempoDePrediccion / resolution; // Calcular el tiempo entre cada punto de la curva
        float time = 0f; // Empezar el tiempo en 0
        Vector3[] positions = new Vector3[resolution]; // Declarar un arreglo de posiciones para guardar las
                                                       // posiciones de la curva
        lineRenderer.positionCount = 0; // Resetear el número de puntos del LineRenderer
        finalPosition = Vector2.zero;
        finalPositionAnt = Vector2.zero;

        for (int i = 0; i < resolution; i++)
        {
            finalPosition =
                initialPosition
                + initialVelocity
                  * time
                + 0.5f
                  * Physics2D.gravity
                  * rb.gravityScale
                  * Mathf.Pow(time, 2); // Calcular la posición final utilizando ecuaciones físicas
                                        // de movimiento parabólico: xf= xo + vo*t + (a*t^2)/2 

            RaycastHit2D hit =
                Physics2D.Raycast(
                    initialPosition,
                    finalPosition - initialPosition,
                    Vector2.Distance(initialPosition, finalPosition),
                    collisionMask); // Realizar un raycast desde la posición inicial hasta la posición final
                                    // para detectar colisiones

            lineRenderer.positionCount++; // Aumentar el número de puntos del LineRenderer
            positions[i] = finalPosition; // Guardar la posición en el arreglo de posiciones

            if (hit) // Si hay una colisión
            {
                RaycastHit2D hit2 =
                Physics2D.Raycast(
                    finalPosition,
                    finalPositionAnt - finalPosition,
                    Vector2.Distance(finalPositionAnt, finalPosition),
                    collisionMask);//si el raycast anterior detecta un collider crea un rayo adicional
                                   // para calcular con precisión el segmento final de la línea

                if (hit2)
                {
                    positions[i] = hit2.point; // Guardar la posición de colisión en el arreglo de posiciones
                    break; // Salir del ciclo si la trayectoria se dibujó hasta un collider en el layer correcto
                }

            }
            time += timeStep; // Aumentar el tiempo
            finalPositionAnt = finalPosition;//guarda el punto final por si tiene que calcular el último segmento
        }

        lineRenderer.SetPositions(positions); // Establecer las posiciones calculadas como las posiciones
                                              // del LineRenderer
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        lineRenderer.positionCount = 0; // Resetear el número de puntos del LineRenderer al entrar en una colisión
    }
}
