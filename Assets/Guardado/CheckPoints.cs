using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int indexCP;
    public bool checkPointActivo;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private ControladorEscena encargado;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !checkPointActivo)
        {
           MasterGameManager.instance.ActivarCheckPoint(indexCP);
            checkPointActivo = true;
            Debug.Log("hola acabas de pasar por aqui :D");
            encargado.ChPoint = indexCP;
        }
    }
    public void CambiarEstadoBandera()
    {
        checkPointActivo = !checkPointActivo;
        if (checkPointActivo) 
        { 
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
    }
}
