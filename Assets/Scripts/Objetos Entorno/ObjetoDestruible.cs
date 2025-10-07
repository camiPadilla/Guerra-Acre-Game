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
    public void Damage(int cantidad)
    {
        
        vidas = vidas - cantidad;
        //StartCoroutine("PerderVida");
        if (vidas <= 0)
        {
            switch (tipo)
            {
                case TipoDestruible.Caja:
                    SoundEvents.DestruirObjeto?.Invoke(transform.position.x, 0);
                    break;

                case TipoDestruible.Hierba:
                    SoundEvents.DestruirObjeto?.Invoke(transform.position.x, 1);
                    break;

                case TipoDestruible.Tronco:
                    SoundEvents.DestruirObjeto?.Invoke(transform.position.x, 2);
                    break;
            }

            //Debug.Log("se destruyo");
            SendMessage("ActivarLoot", SendMessageOptions.DontRequireReceiver);
            gameObject.SetActive(false);
            
        }    
    }
    /*IEnumerator PerderVida()
    {
        if(gameObject.compo)
        Color inicial = GetComponent<SpriteShapeRenderer>().color;
        GetComponent<SpriteShapeRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteShapeRenderer>().color = inicial;
    }*/

}
