using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class ArmaEnemigo : MonoBehaviour
{
    public float fuerza;
    public Rigidbody2D rbArma;
    [Header("Tipo de Arma")]
    public bool fusil; // true si es fusil, false si es bomba

    public void Awake()
    {
        rbArma = GetComponent<Rigidbody2D>();
    }
    public abstract void DisparoIA();

}
