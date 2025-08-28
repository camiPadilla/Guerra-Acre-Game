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

    public void Destuirme()
    {
        Debug.Log("perdio una vida");
        vidas--;
        if (vidas == 0)
        {
            Debug.Log("se destruyo");
            gameObject.SetActive(false);
 
            //Destroy(this.gameObject);
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
