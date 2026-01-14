using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class ControladorVideo : MonoBehaviour
{
    public Toggle pantCompleta;
    public TMP_Dropdown dropCalidad;
    public TMP_Dropdown dropResoluciones;
    public int calidad;
    Resolution[] resoluciones;

    [SerializeField] TMP_Text scoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        calidad = PlayerPrefs.GetInt("numeroDeCalidad", 3);
        dropCalidad.value = calidad;
        AjustarCalidad();

        pantCompleta.isOn = PlayerPrefs.GetInt("pantallaCompleta", 1) == 1;
        Screen.fullScreen = pantCompleta.isOn;

        RevisarResoluciones();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
        PlayerPrefs.SetInt("pantallaCompleta", pantallaCompleta ? 1 : 0);
    }

    public void AjustarCalidad() {
        QualitySettings.SetQualityLevel(dropCalidad.value);
        PlayerPrefs.SetInt("numeroDeCalidad", dropCalidad.value);
        calidad = dropCalidad.value;
    }
    public void RevisarResoluciones()
    {
        resoluciones = Screen.resolutions;
        dropResoluciones.ClearOptions();

        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opciones.Add(opcion);

            if (resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }

        dropResoluciones.AddOptions(opciones);

        int guardada = PlayerPrefs.GetInt("numeroResolucion", resolucionActual);
        dropResoluciones.value = guardada;
        dropResoluciones.RefreshShownValue();
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        PlayerPrefs.SetInt("numeroResolucion", indiceResolucion);
        Resolution r = resoluciones[indiceResolucion];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
    }
}
