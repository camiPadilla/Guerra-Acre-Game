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
    [SerializeField] private BoxCollider boxColliderVeneno;
    [SerializeField] private BoxCollider boxColliderHerido;
    [SerializeField] private Animator animatorVeneno;
    [SerializeField] private Animator animatorHerido;
    // Start is called before the first frame update
    void Start()
    {
        HUDManager.instancia.ActualizarVida(vidasJugador);
        HUDManager.instancia.ActualizarArmadura(vidasEXtras);
        if (boxColliderHerido != null || boxColliderVeneno!=null){
        animatorVeneno = boxColliderVeneno.gameObject.GetComponent<Animator>();
            animatorHerido = boxColliderVeneno.gameObject.GetComponent<Animator>();
        }
        boxColliderVeneno.enabled = false;

        
        RegresarCheckPoint();

        
    }
    IEnumerator ActivarColliderVeneno()
    {
        boxColliderVeneno.enabled = true;
        yield return new WaitForSeconds(1f);
        animatorVeneno.SetTrigger("Hide");
        yield return new WaitForSeconds(0.5f);
        boxColliderVeneno.enabled = false;

    }
    public void ActivarHerido(bool activado)
    {
        if (!activado)
        {
            StartCoroutine(DesactivarHerido());
            return;
        }
        boxColliderHerido.enabled = activado;
        
    }
    IEnumerator DesactivarHerido()
    {
        animatorHerido.SetTrigger("Hide");
        yield return new WaitForSeconds(0.5f);
        boxColliderHerido.enabled = false;
    }
    public void PerderVida(int damage)
    {
        if(damage == 0 && !invulnerabilidad){
           StartCoroutine(ActivarColliderVeneno());
            damage = 1;
        }
        if (vidasEXtras > 0 && !invulnerabilidad)
        {
            vidasEXtras-=damage;
            HUDManager.instancia.ActualizarArmadura(vidasEXtras);
        }
        else if(!invulnerabilidad)
        {            
            vidasJugador -=damage;
            if (vidasJugador > 0) SoundEvents.DanoPersonaje?.Invoke();//Sound by Chelo :D
            HUDManager.instancia.ActualizarVida(vidasJugador);
        }
        
        if (vidasJugador <= 0)
        {
            SoundEvents.MorirPersonaje?.Invoke(); //Sound by Chelo :D
            gameObject.SetActive(false);
            //HUDManager.
        }
        else if(!invulnerabilidad)
        {
            StartCoroutine("Invulnerable");
        }
        if (vidasJugador == 1)
        {
            Debug.Log("mi vida es uno ayuda");
            ActivarHerido(true);
        }
        

    }
    public void Respawn()
    {
        vidasJugador = 6;
        ActivarHerido(false);
        gameObject.SetActive(true);
        RegresarCheckPoint();
        HUDManager.instancia.ActualizarVida(vidasJugador);

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
            ActivarHerido(false);
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
    public void RegresarCheckPoint()
    {
        transform.position = ultimoCheckPoint.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Respawn"))
        {
            if (ultimoCheckPoint != null)
            {
                ultimoCheckPoint.GetComponent<CheckPoints>().CambiarEstadoBandera();
            }
            ultimoCheckPoint = collision.gameObject;
            ultimoCheckPoint.GetComponent<CheckPoints>().CambiarEstadoBandera();
        }
    }
}
