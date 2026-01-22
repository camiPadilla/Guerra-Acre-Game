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
    public TMP_Text textScore;
    public int score;

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
            GameData data = SaveLoadSystem.LoadGame(slotId);

            botonGuardado.SetActive(true);
            texto.text = "Partida " + slotId;

            textInfo.text = data.lastSceneName;   
            textScore.text = data.scoreTotal.ToString(); 

            newButton.gameObject.SetActive(false);
        }
        else
        {
            botonGuardado.SetActive(false);
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

        manager.LoadGame(slotId);
    }

    public void NuevaPartida()
    {
        manager.SetSlot(slotId);
        manager.NewGame();
    }

    public void Eliminar()
    {
        SaveLoadSystem.DeleteSlot(slotId);
        ActualizarVisual();
    }
}