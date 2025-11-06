using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ControladorColeccionables : MonoBehaviour
{
    [SerializeField] GameObject PantallaNota;
    [SerializeField] List<NotasSO> notasLista;
    [SerializeField] UnityEngine.UI.Image imagenNota;
    [SerializeField] GameObject mensajeNota;
    private int indiceNota;
    // Start is called before the first frame update
    void Start()
    {
        ActualizarListaNotas();
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MostrarAnteriorNota();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MostrarSiguienteNota();
        }
    }

    public void MosstrarNota(NotasSO nota)
    {

        Debug.Log("hola si estas entrando ");
        if (MasterGameManager.instance.ObtenerNotas().Count !=0)
        {
            foreach (var notas in notasLista)
            {
                
                if (notas.ID == nota.ID)
                {
                    Debug.Log("Nota ya obtenida");
                    PantallaNota.SetActive(true);
                    imagenNota.sprite = nota.notaImagen;
                    indiceNota = notasLista.IndexOf(notas);
                }
                else
                {
                    StartCoroutine("FadeOut", 1f);
                }
            }
        }
        else
        {
            Debug.Log("no tienes esta nota chaval");
        }
        
    }
    public void MostrarSiguienteNota()
    {
        if(indiceNota != notasLista.Count -1)
        {
            NotasSO notaSiguiente = notasLista[indiceNota+1];
            indiceNota++;
            imagenNota.sprite = notaSiguiente.notaImagen;
        }
        
    }
    public void MostrarAnteriorNota()
    {
        if (indiceNota != 0)
        {
            NotasSO notaAnterior = notasLista[indiceNota-1];
            indiceNota--;
            imagenNota.sprite = notaAnterior.notaImagen;
        }

    }

    public void ActualizarListaNotas()
    {
        notasLista = MasterGameManager.instance.ObtenerNotas();
    }
    IEnumerator FadeOut(float tiempoTotal)
    {
        mensajeNota.SetActive(true);
        UnityEngine.UI.Image imagen = mensajeNota.GetComponent<UnityEngine.UI.Image>();
        float startTime = Time.time;
        while (Time.time - startTime < tiempoTotal)
        {
            float porcentaje = (Time.time - startTime) / tiempoTotal;
            Color color = imagen.color;
            color.a = Mathf.Lerp(1f, 0f, porcentaje);
           imagen.color = color;
            yield return null;
        }

        Color final = imagen.color;
        final.a = 0f;
        imagen.color = final;
        yield return new WaitForSeconds(0.5f);
        mensajeNota.SetActive(false);
    }
}
