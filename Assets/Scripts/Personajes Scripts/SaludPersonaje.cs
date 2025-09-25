using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SaludPersonaje : MonoBehaviour
{
    [SerializeField] int vidasJugador = 6;
    [SerializeField] int vidasEXtras = 0;
    bool invulnerabilidad;
    // Start is called before the first frame update
    void Start()
    {
      
        HUDManager.instancia.ActualizarVida(vidasJugador);
        HUDManager.instancia.ActualizarArmadura(vidasEXtras);

        
    }

    public void PerderVida()
    {
        if (vidasEXtras > 0 && !invulnerabilidad)
        {
            vidasEXtras--;
            HUDManager.instancia.ActualizarArmadura(vidasEXtras);
        }
        else if(!invulnerabilidad)
        {
            vidasJugador--;
            HUDManager.instancia.ActualizarVida(vidasJugador);
        }
        
        if (vidasJugador == 0)
        {
            gameObject.SetActive(false);
            HUDManager.instancia.MostrarPantallaMuerte();

        }
        else if(!invulnerabilidad)
        {
            StartCoroutine("Invulnerable");
        }

    }

    IEnumerator Invulnerable()
    {
        Color colorInvulnerable = Color.black;
        
        Debug.Log("el jugador es invulnerable");
        invulnerabilidad = true;
        GetComponent<SpriteRenderer>().color = colorInvulnerable;
        yield return new WaitForSeconds(10f);
        GetComponent<SpriteRenderer>().color = Color.white;
        invulnerabilidad = false;
        Debug.Log("el jugador ya no es invulnerable");
    }
    public void Curarse()
    {
        if (vidasJugador > 4)
        {
            vidasJugador = 6;
        }
        else
        {
            vidasJugador++;
        }
        HUDManager.instancia.ActualizarVida(vidasJugador);

        
    }
    public void ObtenerArmadura()
    {
        if (vidasEXtras == 1)
        {
            vidasEXtras = 2;
        }
        else
        {
            vidasEXtras = 1;
        }
        HUDManager.instancia.ActualizarArmadura(vidasEXtras);
        
    }
}
