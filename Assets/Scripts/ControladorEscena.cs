using PantallaCarga;
using System.Collections;
using TarodevController;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEscena : MonoBehaviour
{
    [Header("Referencias de escena")]
    public int sceneIndex;
    public string nameScene;

    [SerializeField] LoaderScene loaderScene;
    [SerializeField] MasterGameManager masterGameManager;
    [SerializeField] SaludPersonaje vidas;
    [SerializeField] InventarioManager cantidadNotas;
    [SerializeField] AtaquePersonaje balas;
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerController player;

    [Header("UI")]
    [SerializeField] GameObject MenuInGame;
    [SerializeField] GameObject HUD;
    [SerializeField] HUDManager hudManager;

    [Header("UI Textos")]
    public TMP_Text textVidas;
    public TMP_Text textNot;
    public TMP_Text textBalas;
    public TMP_Text textEnemigos;

    [Header("Checkpoints")]
    public int ChPoint;

    private void Start()
    {
        if (loaderScene == null) loaderScene = FindObjectOfType<LoaderScene>();
        if (hudManager == null) hudManager = FindObjectOfType<HUDManager>();
        if (masterGameManager == null) masterGameManager = FindObjectOfType<MasterGameManager>();

        if (hudManager != null) HUD = hudManager.gameObject;
        if (MenuInGame == null) MenuInGame = GameObject.FindWithTag("canvas");

        if (HUD != null) HUD.SetActive(true);
        if (MenuInGame != null) MenuInGame.SetActive(false);

        switch (sceneIndex)
        {
            case 0:
                nameScene = ConstantsGame.SCENEMAINMENU;
                break;
            case 1:
                nameScene = ConstantsGame.SCENAUNO;
                hudManager?.Bienvenido();
                break;
            case 2:
                nameScene = ConstantsGame.SCENADOS;
                hudManager?.Bienvenido();
                break;
            case 3:
                nameScene = ConstantsGame.SCENECREDITS;
                break;
        }
    }

    private void Update()
    {
        if (sceneIndex != 0)
        {
            ActualizarHUD();
        }
    }

    private void ActualizarHUD()
    {
        if (vidas != null) textVidas.text = vidas.vidasJugador.ToString();
        if (cantidadNotas != null) textNot.text = cantidadNotas.cantNotas.ToString();
        if (balas != null) textBalas.text = balas.GettotalBalas().ToString();
        if (gameManager != null) textEnemigos.text = gameManager.enemigosMuertos.ToString();
    }

    public void SiguienteNivel()
    {
        if (HUD != null) HUD.SetActive(false);

        switch (sceneIndex)
        {
            case 1:
                loaderScene.LoadSceneString(ConstantsGame.SCENADOS);
                break;
            case 2:
                loaderScene.LoadSceneString(ConstantsGame.SCENECREDITS);
                break;
        }
    }

    public void IrMenu()
    {
        loaderScene.LoadSceneString(ConstantsGame.SCENEMAINMENU);

        // Destruye objetos persistentes para limpiar
        Destroy(MenuPausa.instance.gameObject);
        Destroy(HUDManager.instancia.gameObject);
        Destroy(MenuInGame);
        Destroy(masterGameManager?.gameObject);
    }

    public void ReiniciarNivel()
    {
        if (sceneIndex == 1)
            loaderScene.LoadSceneString(ConstantsGame.SCENAUNO);
        else if (sceneIndex == 2)
            loaderScene.LoadSceneString(ConstantsGame.SCENADOS);
    }

    public void Salir()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // ---------------- GUARDADO ----------------

    public void GuardarPartida()
    {
        if (masterGameManager == null)
        {
            masterGameManager = FindObjectOfType<MasterGameManager>();
        }

        GameData data = new GameData(
            vidas,
            balas,
            player,
            sceneIndex,
            nameScene,
            ChPoint,
            masterGameManager.currentSlot
        );

        SaveLoadSystem.SaveGame(data, masterGameManager.currentSlot);
        Debug.Log("Partida guardada en slot " + masterGameManager.currentSlot);
    }

    public void CrearPart()
    {
        masterGameManager?.NewGame();
    }
}
