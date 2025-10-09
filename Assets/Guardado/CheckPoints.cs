using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int indexCP;
    public bool checkPointActivo;
    [SerializeField] GameManager gameManager;
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
            //gameManager.ActivarCheckPoint(indexCP);
            checkPointActivo = true;
            Debug.Log("hola acabas de pasar por aqui :D");
        }
    }
}
