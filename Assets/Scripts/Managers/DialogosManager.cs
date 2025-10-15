using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogosManager : MonoBehaviour
{
    [SerializeField] RectTransform pantallaDialogo;
    [SerializeField] TextMeshProUGUI cajaDeDialogo;
    [SerializeField] string dialogoActual;
    [SerializeField] float velocidad;
    bool dialogando = false;
    // Start is called before the first frame update
    
    public void IniciarDialogo(DialogosSO dialogo)
    {
        dialogando = true;
        dialogoActual = dialogo.texto;
        LimpiarCaja();
        StartCoroutine(MostrarMensaje());
    }
    private void Update()
    {
        if (Input.anyKeyDown && dialogando == true && !GameManager.instancia.Onplaying())
        {
            if (FraseEnProceso())
            {
                FinalizarDialogo();
            }
            else
            {
                dialogando = false;
                GameManager.instancia.CerrarEstado();
               

            }
        }
    }
    bool FraseEnProceso()
    {
        return dialogoActual != null;
    }
    void FinalizarDialogo()
    {
        StopAllCoroutines();
        cajaDeDialogo.text = dialogoActual;
        dialogoActual= null;
    }
    void LimpiarCaja()
    {
        cajaDeDialogo.text = "";
    }
        
    private IEnumerator MostrarMensaje()
    {
        cajaDeDialogo.text = "";
        foreach( char letra in dialogoActual.ToCharArray())
        {
            cajaDeDialogo.text += letra;
            yield return new WaitForSeconds(velocidad);
        }
        FinalizarDialogo();
    }
}
