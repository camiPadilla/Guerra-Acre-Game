using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int indexCP;
    public bool checkPointActivo;
    [SerializeField] MasterGameManager mastergameManager;
    // Start is called before the first frame update
    void Start()
    {
        mastergameManager = FindObjectOfType<MasterGameManager>();
    }

    // Update is called once per frame
    void AutoSave()
    {
        mastergameManager.checkpointsActivos[indexCP] = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !checkPointActivo)
        {
            //gameManager.ActivarCheckPoint(indexCP);
            checkPointActivo = true;
            Debug.Log("hola acabas de pasar por aqui :D");
            AutoSave();
        }
    }
}
