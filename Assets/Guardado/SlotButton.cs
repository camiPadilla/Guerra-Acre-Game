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
            texto.text = "Partida guardada";
            deleteButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(true);
        }
        else
        {
            fondo.sprite = spriteVacio;
            texto.text = "Vacío";
            deleteButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(false); // puedes dejarlo activo para "Nuevo juego"
        }
    }

    public void Cargar()
    {
        if (manager == null)
            manager = FindObjectOfType<MasterGameManager>();

        if (SaveLoadSystem.ExisteGuardado(slotId))
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