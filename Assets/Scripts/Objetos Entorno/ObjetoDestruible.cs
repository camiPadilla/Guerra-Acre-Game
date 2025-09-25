using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ObjetoDestruible : MonoBehaviour
{
    [SerializeField] int vidas;
    public void Damage(int cantidad)
    {
        //Debug.Log("perdio una vida");
        vidas = vidas - cantidad;
        if (vidas <= 0)
        {
            //Debug.Log("se destruyo");
            gameObject.SetActive(false);
        }    
    }
}
