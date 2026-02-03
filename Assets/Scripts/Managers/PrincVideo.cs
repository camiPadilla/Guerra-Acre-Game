using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PrincVideo : MonoBehaviour
{
    public TMP_Dropdown dropCalidad;
    public TMP_Dropdown dropResoluciones;
    public Toggle pantCompleta;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CargarDesdePrefs()
    {
        if (dropCalidad)
            dropCalidad.value = PlayerPrefs.GetInt("numeroDeCalidad");
        if (dropResoluciones)
            dropResoluciones.value = PlayerPrefs.GetInt("numeroResolucion");
        if (pantCompleta)
            pantCompleta.isOn = PlayerPrefs.GetInt("pantallaCompleta", 0) == 1;
    }
}
