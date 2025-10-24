using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour
{
    public int slotId;
    public Image fondo;
    public TMP_Text texto;
    public Sprite spriteVacio;
    public Sprite spriteOcupado;
    public Button deleteButton;
    public Button startButton;

    [SerializeField] private MasterGameManager manager;

    void Start()
    {
        if (manager == null)
            manager = FindObjectOfType<MasterGameManager>();

        ActualizarVisual();
    }

    public void ActualizarVisual()
    {
        if (SaveLoadSystem.ExisteGuardado(slotId))
        {
            fondo.sprite = spriteOcupado;

            GameData data = SaveLoadSystem.LoadGame(slotId);
            if (data != null)
            {
                texto.text = "Nivel " + manager.currentLevel.ToString();
                deleteButton.gameObject.SetActive(true);
                startButton.gameObject.SetActive(true);
            }
        }
        else
        {
            fondo.sprite = spriteVacio;
            texto.text = "Vacío";
            deleteButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(false);
        }
    }

    public void Cargar()
    {
        if (manager == null)
            manager = FindObjectOfType<MasterGameManager>();

        if (SaveLoadSystem.ExisteGuardado(slotId))
        {
            manager.SetSlot(slotId);
            manager.LoadGame(); // que haga todo el proceso: leer, cargar escena y restaurar jugador
        }
    }

    public void Eliminar()
    {
        SaveLoadSystem.DeleteSlot(slotId);
        ActualizarVisual(); // actualiza sprite y texto después de eliminar
    }
}