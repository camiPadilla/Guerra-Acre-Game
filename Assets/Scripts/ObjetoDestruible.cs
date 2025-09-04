using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class ObjetoDestruible : MonoBehaviour
{
    [SerializeField] int vidas;
    // Start is called before the first frame update
    

    public void Destuirme()
    {
        Debug.Log("perdio una vida");
        vidas--;
        if (vidas == 0)
        {
            Debug.Log("se destruyo");
            
            gameObject.SetActive(false);
 
        }
        
            
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("hitbox"))
        {
            Destuirme();
        }
    }
    
}
