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
    public Button newButton;
    public TMP_Text textButton;

    [SerializeField] private MasterGameManager manager;

    void Start()
    {
        if (manager == null)
            manager = FindObjectOfType<MasterGameManager>();

        ActualizarVisual();
    }

    public void ActualizarVisual()
    {
        if (SaveLoadSystem.HasSave(slotId))
        {
            fondo.sprite = spriteOcupado;
            texto.text = "Partida ";
            deleteButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(true);
            newButton.gameObject.SetActive(false);
        }
        else
        {
            fondo.sprite = spriteVacio;
            textButton.text = "Nueva partida";
            deleteButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(false);
        }
    }

    public void Cargar()
    {
        if (manager == null)
            manager = FindObjectOfType<MasterGameManager>();

        if (SaveLoadSystem.HasSave(slotId))
        {
            manager.SetSlot(slotId);
            manager.LoadGame();
        }
        else
        {
            manager.SetSlot(slotId);
        }
    }

    public void Eliminar()
    {
        SaveLoadSystem.DeleteSlot(slotId);
        ActualizarVisual();
    }
}