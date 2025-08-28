using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeManager : MonoBehaviour
{
    [SerializeField] MovPersonaje scriptMovimiento;
    [SerializeField] AtaquePersonaje scriptAtaque;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scriptAtaque.SetDireccion(scriptMovimiento.GetDireccion());
    }
}
