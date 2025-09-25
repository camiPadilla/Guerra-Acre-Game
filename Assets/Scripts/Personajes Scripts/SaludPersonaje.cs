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
        Debug.Log("el jugador tiene en vidas " + vidasJugador);
        HUDManager.instancia.ActualizarVida(vidasJugador);
        HUDManager.instancia.ActualizarArmadura(vidasEXtras);

        Debug.Log("el jugador tiene vidas extas " + vidasEXtras);
    }

    public void PerderVida()
    {
        if (vidasEXtras > 0 && !invulnerabilidad)
        {
            vidasEXtras--;
            HUDManager.instancia.ActualizarArmadura(vidasEXtras);
        }
        else
        {
            vidasJugador--;
            HUDManager.instancia.ActualizarVida(vidasJugador);
        }
        Debug.Log("el jugador tiene en vidas " + vidasJugador);
        Debug.Log("el jugador tiene vidas extas " + vidasEXtras);
        if (vidasJugador == 0)
        {
            gameObject.SetActive(false);
            HUDManager.instancia.MostrarPantallaMuerte();

        }
        else
        {
            StartCoroutine("Invulnerable");
        }

    }

    IEnumerator Invulnerable()
    {
        invulnerabilidad = true;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(3f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
        invulnerabilidad = false;
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

        Debug.Log("jugador gano una vida, tiene " + vidasJugador);
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
        Debug.Log("el jugador tiene vidas extas " + vidasEXtras);
    }
}
