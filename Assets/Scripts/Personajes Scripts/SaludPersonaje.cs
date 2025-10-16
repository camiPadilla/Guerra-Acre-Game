using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SaludPersonaje : MonoBehaviour
{
    public int vidasJugador = 6;
    public int vidasEXtras = 0;
    bool invulnerabilidad;
    [SerializeField] float tiempoInvulnerable;
    [SerializeField] private GameObject ultimoCheckPoint;
    // Start is called before the first frame update
    void Start()
    {
      
        HUDManager.instancia.ActualizarVida(vidasJugador);
        HUDManager.instancia.ActualizarArmadura(vidasEXtras);

        
    }

    public void PerderVida(int damage)
    {
        if (vidasEXtras > 0 && !invulnerabilidad)
        {
            vidasEXtras-=damage;
            HUDManager.instancia.ActualizarArmadura(vidasEXtras);
        }
        else if(!invulnerabilidad)
        {
            vidasJugador-=damage;
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
        
        Debug.Log("el jugador es invulnerable");
        invulnerabilidad = true;
        yield return new WaitForSeconds(tiempoInvulnerable);
        
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
