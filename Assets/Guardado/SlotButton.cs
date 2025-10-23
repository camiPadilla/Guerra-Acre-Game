using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour
{
    public int slotId;
    public Image fondo;
    public TMP_Text texto;
    public Sprite spritevacio;
    public Sprite spriteOcupado;
    public Button delete;
    public Button start;

    [SerializeField] MasterGameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        ActualizarVisual();
    }
    public void ActualizarVisual()
    {
        if (SaveLoadSystem.ExisteGuardado(slotId))
        {
            fondo.sprite = spriteOcupado;
            texto.text = "Nivel " + manager.escenaActual;
            delete.gameObject.SetActive(true);
            start.gameObject.SetActive(true); 
        }
        else
        {
            fondo.sprite = spritevacio;
            texto.text = "Vacio";
            delete.gameObject.SetActive(false);
            start.gameObject.SetActive(false);
        }
    }
    public void Cargar()
    {
        if (SaveLoadSystem.ExisteGuardado(slotId))
        {
            SaveLoadSystem.LoadGame(slotId);
        }
    }
}
