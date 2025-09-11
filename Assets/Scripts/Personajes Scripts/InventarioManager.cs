using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioManager : MonoBehaviour
{
    int balas=0;
    int piedras;
    int dinamitas;
    [SerializeField] int aumento;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetBalas()
    {
        return balas;
       
    }
    public void SetBalas()
    {
        balas--;
    }
    public void RecibirInfo(string nombre)
    {
        switch (nombre)
        {
            case "balas":
                balas += aumento;
                Debug.Log("ahora el jugador tiene en balas " + balas);
                break;
            case "botiquin":
                SendMessage("Curarse");
                break;
            case "armadura":
                SendMessage("ObtenerArmadura");
                break;
            case "dinamita":
                dinamitas++;
                Debug.Log("el jugador tiene en dinamita " + dinamitas);
                break;
        }
       
        
    }
}
