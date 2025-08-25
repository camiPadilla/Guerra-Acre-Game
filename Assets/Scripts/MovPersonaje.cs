using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] float fuerzaSalto;
    [SerializeField] Rigidbody2D miRigid;
    bool enSuelo;
    [SerializeField]LayerMask capaSuelo;
    [SerializeField] float distanciaRayCast = 0.1f;
    [SerializeField] Transform puntoRayCast;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float entradaX = Input.GetAxis("Horizontal");
        if (entradaX != 0)
        {
            miRigid.velocity = miRigid.velocity + Vector2.right * entradaX * velocidad * Time.deltaTime;
        }
        DetectarSuelo();
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            miRigid.AddForce(Vector2.up * fuerzaSalto);
            enSuelo = false;
        }


    }
    void DetectarSuelo()
    {
        // Lanzar RayCast hacia abajo para detectar suelo
        RaycastHit2D hit = Physics2D.Raycast(puntoRayCast.position, Vector2.down, distanciaRayCast, capaSuelo);

        // Visualizar el RayCast en el editor (solo para debugging)
        Debug.DrawRay(puntoRayCast.position, Vector2.down * distanciaRayCast, Color.red);

        // Comprobar si el RayCast golpeó algo
        if (hit.collider != null)
        {
            enSuelo = true;
        }
        else
        {
            enSuelo = false;
        }
    }
}
