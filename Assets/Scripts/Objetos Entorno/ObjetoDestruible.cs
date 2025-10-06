using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class ObjetoDestruible : MonoBehaviour
{
    [SerializeField] int vidas;
    public void Damage(int cantidad)
    {
        //Debug.Log("perdio una vida");
        vidas = vidas - cantidad;
        //StartCoroutine("PerderVida");
        if (vidas <= 0)
        {
            SoundEvents.DestruirObjeto?.Invoke(transform.position.x);

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
