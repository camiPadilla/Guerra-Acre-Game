using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiedraEnemigo : MonoBehaviour
{
    private Rigidbody2D rbPiedra;
    [SerializeField] private float fuerza;
    // Start is called before the first frame update
    void Start()
    {
        rbPiedra = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rbPiedra.AddForce(Vector2.left * fuerza, ForceMode2D.Impulse);
    }
    
}
