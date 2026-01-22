using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TarodevController;
using PantallaCarga;
using System;

public class MasterGameManager : MonoBehaviour
{
    public static MasterGameManager instance;

    public SaludPersonaje playerSalud;
    public AtaquePersonaje playerAtaque;
    public PlayerController playerController;
    public LoaderScene loaderScene;
    [SerializeField] List<NotasSO> notasObtenidas = new List<NotasSO>();
    [SerializeField] NotasSO notaVacia;
    public List<bool> checkpointsActivos = new List<bool>();
    public GameData gameData;
    public String nEscena;
    public int scoreFinal;

    public string escenaActual;
    public string escenaSiguiente;
    public bool InGame;
    public int lastCP;
    public int AcPoint;
    public int currentLevel = 1;
    
    [SerializeField] GameObject menuInGame;
    [SerializeField] GameObject menuPausa;
    [SerializeField] GameObject menuScript;
    //[SerializeField] GameObject plape;
    
    //private string IdNotas[];
    [SerializeField] public int currentSlot; 
    private void Start()
    {
        InicializarLista();
    }
    IEnumerator IniJuego()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }    public void InicializarLista()
    {
        for(int i= 0;i<14; i++)
        {
            notasObtenidas.Add(notaVacia);
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }


        instance = this;
        DontDestroyOnLoad(gameObject);

        ReferenciasPlayer();
        SceneManager.sceneLoaded += OnLoadScene;
    }
    public void AsignarMenuPausa(GameObject menu)
    {
        menuInGame = menu;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instancia != null)
        {
            EsPa();
        }

    }
    public void EsPa()
    {
            Debug.Log("Pausando juego desde MasterGM");
            GameManager.instancia.CerrarEstado();
    }
    public void PausarOtravez()
    {
         menuPausa.SetActive(true);
    }
    public void Despausar()
    {

        menuPausa.SetActive(false);

    }

    private void ReferenciasPlayer()
    {
        playerSalud = FindObjectOfType<SaludPersonaje>();
        playerAtaque = FindObjectOfType<AtaquePersonaje>();
        playerController = FindObjectOfType<PlayerController>();

    }

    public void SetSlot(int slot)
    {
        currentSlot = slot;
        Debug.Log("Slot seleccionado: " + currentSlot);
    }

    private void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        ReferenciasPlayer();
        if(scene.name == "PantallaTiny")
        {
            StartCoroutine(IniJuego());
        }
        if (scene.name != "MainMenu" && gameData != null)
        {
            StartCoroutine(LoadRestore(gameData));
            scoreFinal = gameData.scoreTotal;
            nEscena = gameData.lastSceneName;

            gameData = null; 
        }
        if (scene.name == "MainMenu")
        {
            menuInGame.SetActive(false);
            Debug.Log("Estas en el Main Menu");
            return;
        }
        // Configuración según la escena
        if (scene.name == "EscenaUno")
        {
            escenaActual = "EscenaUno";
            escenaSiguiente = "EscenaDos";
            menuInGame.SetActive(true);
            menuPausa.SetActive(false);
            nEscena = "Puerto Alonso";
        }
        else if (scene.name == "EscenaDos")
        {
            escenaActual = "EscenaDos";
            escenaSiguiente = "Creditos";
            menuInGame.SetActive(true);
            menuPausa.SetActive(false);
            nEscena = "Riosinho";
        }
    }

    public void ActivarCheckPoint(int index)
    {
        if (gameData == null)
            gameData = new GameData(playerSalud, playerAtaque, playerController, currentLevel, SceneManager.GetActiveScene().name, lastCP, currentSlot, scoreFinal, nEscena);

        // Asegurar que la lista sea suficientemente larga
        if (index >= gameData.checkpointsActivos.Count)
        {
            for (int i = gameData.checkpointsActivos.Count; i <= index; i++)
                gameData.checkpointsActivos.Add(false);
        }

        gameData.checkpointsActivos[index] = true;
        gameData.lastCheckPoint = index;

        SaveGame();
        Debug.Log(" Checkpoint activado y guardado: " + index);
    }

    public void SaveGame()
    {
        if (gameData == null)
        {
            gameData = new GameData(playerSalud, playerAtaque, playerController,
                currentLevel, SceneManager.GetActiveScene().name, lastCP,
                currentSlot, scoreFinal, nEscena);
        }

        SaveLoadSystem.SaveGame(gameData, currentSlot);
        Debug.Log($"Juego guardado en slot {currentSlot}");
    }


    public void LoadGame(int slot)
    {
        GameData data = SaveLoadSystem.LoadGame(slot);
        if (data == null) return;

        StartCoroutine(RestoreAfterLoad(data));
    }

    private IEnumerator LoadRestore(GameData data)
    {
        if (string.IsNullOrEmpty(data.lastScene))
        {
            yield break;
        }
        if(SceneManager.GetActiveScene().name != data.lastScene)    {
            loaderScene.LoadSceneString(data.lastScene);

            yield break;
        }

        yield return new WaitForSeconds(0.2f);

        ReferenciasPlayer();
        yield return RestoreAfterLoad(data);
    }

    private IEnumerator RestoreAfterLoad(GameData data)
    {
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForFixedUpdate();
        ReferenciasPlayer();
        playerController.enabled = false;

        Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.simulated = false;

        if (data.lastCheckPoint >= 0)
        {
            CheckPoints[] cps = FindObjectsOfType<CheckPoints>();

            foreach (CheckPoints cp in cps)
            {
                if (cp.indexCP == data.lastCheckPoint)
                {
                    rb.position = cp.transform.position;
                    break;
                }
            }
        }
        playerSalud.vidasJugador = data.vidasJugador;
        playerSalud.vidasEXtras = data.vidasExtras;
        playerAtaque.cantidadBalas = data.balas;
        playerAtaque.seleccionArma = data.tipoArma;

        rb.simulated = true;
        yield return new WaitForFixedUpdate();
        playerController.enabled = true;
    }

    public void NewGame()
    {
        if (currentSlot <= 0)
        {
            Debug.LogError("Slot no seleccionado");
            return;
        }

        SaveLoadSystem.DeleteSlot(currentSlot);
        scoreFinal = 0;
        loaderScene.LoadSceneString(ConstantsGame.SCENAUNO);
    }


    public void DeleteGame()
    {
        SaveLoadSystem.DeleteAllData();
        Debug.Log(" Datos de guardado eliminados completamente.");
    }

    public void DetenerTiempo()
    {
        Time.timeScale = 0;
    }
    public void IrMenu()
    {
        SoundEvents.DetenerMusica?.Invoke();
        loaderScene.LoadSceneString(ConstantsGame.SCENEMAINMENU);

        Destroy(HUDManager.instancia.gameObject);
        Time.timeScale = 1;
    }

    public void AddNota(NotasSO notaNueva)
    {
        int indice = notaNueva.numeroNota;
        notasObtenidas[indice] = notaNueva;
        //notaNueva.SetObtenida(true);
    }
    public List<NotasSO> ObtenerNotas()
    {
        return notasObtenidas;
    }
    public void RecibirScoreFinal(int scoreLvl)
    {
        scoreFinal += scoreLvl;
    }
}