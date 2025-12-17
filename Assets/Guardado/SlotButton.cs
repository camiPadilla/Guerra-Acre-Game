using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour
{
    public int slotId;
    public Image fondo;
    public TMP_Text texto;
    public TMP_Text textInfo;
    public Button newButton;
    [SerializeField] GameObject botonGuardado;
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
            botonGuardado.SetActive(true);
            texto.text = "Partida " + slotId.ToString();
            textInfo.text = MasterGameManager.instance.nEscena;
            newButton.gameObject.SetActive(false);
        }
        else
        {
            botonGuardado.SetActive(false);
            textButton.gameObject.SetActive(true);
            newButton.gameObject.SetActive(true);
            textButton.text = "Nueva partida";
        }
    }

    public void Cargar()
    {
        if (manager == null)
            manager = FindObjectOfType<MasterGameManager>();

        if (!SaveLoadSystem.HasSave(slotId))
            return;

        manager.SetSlot(slotId);

        GameData data = SaveLoadSystem.LoadGame(slotId);
        manager.gameData = data;

        SceneManager.LoadScene(data.lastScene);
    }


    public void Eliminar()
    {
        SaveLoadSystem.DeleteSlot(slotId);
        ActualizarVisual();
    }
}