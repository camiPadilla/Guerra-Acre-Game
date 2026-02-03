using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PrincVideo : MonoBehaviour
{
    public TMP_Dropdown dropCalidad;
    public TMP_Dropdown dropResoluciones;
    public Toggle pantCompleta;

    void OnEnable()
    {
        StartCoroutine(InitSeguro());
    }

    IEnumerator InitSeguro()
    {
        // Espera a que ControladorVideo exista (BUILD SAFE)
        yield return null;

        if (ControladorVideo.Instance == null)
        {
            Debug.LogError("ControladorVideo NO existe en escena");
            yield break;
        }

        InicializarUI();
        ConectarEventos();
    }

    void InicializarUI()
    {
        // ===== CALIDAD =====
        dropCalidad.SetValueWithoutNotify(
            PlayerPrefs.GetInt("numeroDeCalidad", QualitySettings.GetQualityLevel())
        );

        // ===== PANTALLA COMPLETA =====
        pantCompleta.SetIsOnWithoutNotify(
            PlayerPrefs.GetInt("pantallaCompleta", 1) == 1
        );

        // ===== RESOLUCIONES =====
        Resolution[] res = ControladorVideo.Instance.GetResoluciones();
        dropResoluciones.ClearOptions();

        List<string> opciones = new List<string>();
        for (int i = 0; i < res.Length; i++)
        {
            opciones.Add($"{res[i].width} x {res[i].height}");
        }

        dropResoluciones.AddOptions(opciones);

        int resActual = PlayerPrefs.GetInt("numeroResolucion", res.Length - 1);
        dropResoluciones.SetValueWithoutNotify(resActual);

        dropResoluciones.RefreshShownValue();
        dropCalidad.RefreshShownValue();
    }

    void ConectarEventos()
    {
        dropCalidad.onValueChanged.RemoveAllListeners();
        dropResoluciones.onValueChanged.RemoveAllListeners();
        pantCompleta.onValueChanged.RemoveAllListeners();

        dropCalidad.onValueChanged.AddListener(OnCambiarCalidad);
        dropResoluciones.onValueChanged.AddListener(OnCambiarResolucion);
        pantCompleta.onValueChanged.AddListener(OnPantallaCompleta);
    }

    // ===== EVENTOS =====

    void OnCambiarCalidad(int v)
    {
        ControladorVideo.Instance.SetCalidad(v);
    }

    void OnCambiarResolucion(int v)
    {
        ControladorVideo.Instance.SetResolucion(v);
    }

    void OnPantallaCompleta(bool v)
    {
        ControladorVideo.Instance.SetPantallaCompleta(v);
    }
}
