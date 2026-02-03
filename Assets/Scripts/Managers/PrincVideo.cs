using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class PrincVideo : MonoBehaviour
{
    public TMP_Dropdown dropCalidad;
    public TMP_Dropdown dropResoluciones;
    public Toggle pantCompleta;

    void Start()
    {
        CargarUI();
    }

    void CargarUI()
    {
        dropCalidad.value = PlayerPrefs.GetInt("numeroDeCalidad", 3);
        pantCompleta.isOn = PlayerPrefs.GetInt("pantallaCompleta", 1) == 1;

        Resolution[] res = ControladorVideo.Instance.GetResoluciones();
        dropResoluciones.ClearOptions();

        List<string> opciones = new List<string>();
        for (int i = 0; i < res.Length; i++)
            opciones.Add(res[i].width + " x " + res[i].height);

        dropResoluciones.AddOptions(opciones);
        dropResoluciones.value = PlayerPrefs.GetInt("numeroResolucion", res.Length - 1);
    }

    public void OnCambiarCalidad(int v)
    {
        ControladorVideo.Instance.SetCalidad(v);
    }

    public void OnCambiarResolucion(int v)
    {
        ControladorVideo.Instance.SetResolucion(v);
    }

    public void OnPantallaCompleta(bool v)
    {
        ControladorVideo.Instance.SetPantallaCompleta(v);
    }
}
