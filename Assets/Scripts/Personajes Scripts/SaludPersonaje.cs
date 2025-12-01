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
    private SpriteManager miSprite;

    private Vector3 posicionInicial; //ADDED BY CHELO :D

    // Start is called before the first frame update
    void Start()
    {
        miSprite = GetComponent<SpriteManager>();
        posicionInicial = transform.position; //ADDED BY CHELO :D
        HUDManager.instancia.ActualizarVida(vidasJugador);
        HUDManager.instancia.ActualizarArmadura(vidasEXtras);
        miSprite.EquiparArmadura(vidasEXtras);
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
        Debug.Log("desactivando herido");
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
            miSprite.EquiparArmadura(vidasEXtras);

        }
        else if(!invulnerabilidad)
        {            
            vidasJugador -= damage;
            miSprite.CaraHerido(true);
            if (vidasJugador > 0) SoundEvents.DanoPersonaje?.Invoke();//Sound by Chelo :D
            HUDManager.instancia.ActualizarVida(vidasJugador);
            miSprite.EquiparArmadura(vidasEXtras);

        }

        if (vidasJugador <= 0)
        {
            SoundEvents.MorirPersonaje?.Invoke(); //Sound by Chelo :D
            SoundEvents.DetenerPasosPasto?.Invoke(); //Sound by Chelo :D
            ActivarHerido(false);
            gameObject.SetActive(false);
            HUDManager.instancia.Muerto();
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
        gameObject.SetActive(true);
        RegresarCheckPoint();
        boxColliderHerido.enabled = false;
        boxColliderVeneno.enabled = false;
        //ActivarHerido(false);
        HUDManager.instancia.ActualizarVida(vidasJugador);

    }

    IEnumerator Invulnerable()
    {

        Debug.Log("el jugador es invulnerable");
        invulnerabilidad = true;
        yield return new WaitForSeconds(tiempoInvulnerable);
        miSprite.CaraHerido(false);
        invulnerabilidad = false;
        Debug.Log("el jugador ya no es invulnerable");
    }

    public void DesactivarInvulnerabilidad()
    {
        StopCoroutine("Invulnerable");
        invulnerabilidad = false;
        
    }
    public void Curarse(int cura)
    {
        if (vidasJugador > 5)
        {
            vidasJugador = 6;
        }
        else
        {
            vidasJugador+=cura;
            ActivarHerido(false);
        }
        HUDManager.instancia.ActualizarVida(vidasJugador);

        SoundEvents.RecogerVida.Invoke(); //Sonido by Chelo :D

    }
    public void ObtenerArmadura()
    {
        if (vidasEXtras >= 1)
        {
            vidasEXtras = 2;
        }
        else
        {
            vidasEXtras = 1;
        }
        miSprite.EquiparArmadura(vidasEXtras);
        HUDManager.instancia.ActualizarArmadura(vidasEXtras);

        SoundEvents.EquiparArmadura.Invoke(); //Sonido by Chelo :D
    }
    public void RegresarCheckPoint()
    {
        if (ultimoCheckPoint != null)// ADDED BY CHELO :D
        {
            transform.position = ultimoCheckPoint.transform.position;
        }
        else
        {
            // Si no hay checkpoint, usa la posici�n inicial del personaje
            Debug.LogWarning("No hay checkpoint asignado, regresando al punto inicial.");

            // Puedes guardar la posici�n inicial al comenzar
            transform.position = posicionInicial;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Respawn"))
        {
            if (ultimoCheckPoint != null)
            {
                if (ultimoCheckPoint.GetComponent<CheckPoints>().Verificar(collision.gameObject.GetComponent<CheckPoints>()))
                {
                    ultimoCheckPoint.GetComponent<CheckPoints>().CambiarEstadoBandera();
                    ultimoCheckPoint = collision.gameObject;
                    ultimoCheckPoint.GetComponent<CheckPoints>().CambiarEstadoBandera();
                }
            }
            else
            {
                ultimoCheckPoint = collision.gameObject;
                ultimoCheckPoint.GetComponent<CheckPoints>().CambiarEstadoBandera();
            }

        }

    }
    public int GetArmaduraJugador()
    {
        return vidasEXtras;
    }
}
