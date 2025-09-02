using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ObjetoDestruible : MonoBehaviour
{
    [SerializeField] int vidas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Daño(int cantidad)
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
