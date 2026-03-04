using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.UI;
using TMPro;

public class TutorialObjeto : MonoBehaviour
{
    
    public GameObject botiquin;
    public GameObject armadura;
    public GameObject fusil;
    public GameObject pantallaObj;
    public TMP_Text textoDes;

    public string nombreO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TutorialObjUI()
    {
        GameManager.instancia.CambiarDeEstado(1);
        pantallaObj.SetActive(true);
        if(nombreO == "Botiquin")
        {
            fusil.SetActive(false);
            botiquin.SetActive(true);
            armadura.SetActive(true);
            textoDes.text = "El botiquín restaura vida y la armadura absorbe daño antes de afectar tus corazones.";
        }
        if(nombreO == "Fusil")
        {
            fusil.SetActive(true);
            botiquin.SetActive(false);
            armadura.SetActive(false);
            textoDes.text = "El fusil permite atacar a distancia; administra bien tu munición.";
        }
    }
  

}
