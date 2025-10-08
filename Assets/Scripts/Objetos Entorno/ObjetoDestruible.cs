using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class ObjetoDestruible : MonoBehaviour
{
    public enum TipoDestruible
    {
        Caja,
        Hierba,
        Tronco
    }

    [SerializeField] private TipoDestruible tipo;
    [SerializeField] int vidas;
    SpriteRenderer sprite;
    Color colorInicial;
    [SerializeField] bool caja;

    bool enContacto = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        colorInicial = sprite.color;
    }
    public void Damage(int cantidad)
    {
        
        vidas = vidas - cantidad;
        StartCoroutine("PerderVida");
        if (vidas <= 0)
        {

            SoundEvents.DestruirObjeto?.Invoke(transform.position.x, (int)tipo);

            //Debug.Log("se destruyo");
            SendMessage("ActivarLoot", SendMessageOptions.DontRequireReceiver);
            gameObject.SetActive(false);
            
        }    
    }
    IEnumerator PerderVida()
    {
        Color inicial = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.clear;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = inicial;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !caja )
        {
            StartCoroutine(FeedBackGrafico());
            enContacto = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")  && !caja)
        {
            StopCoroutine(FeedBackGrafico());
            sprite.color = colorInicial;
            enContacto = false;

        }
    }
    IEnumerator FeedBackGrafico()
    {
        while (enContacto)
        {
            sprite.color = Color.Lerp(colorInicial, Color.gray, Mathf.PingPong(Time.time * 3, 1));

            yield return null;
            
            
        }
        
    }
}
