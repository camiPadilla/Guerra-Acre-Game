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
    public bool loadingFromSave = false;
    public int idCinematicaActual;
    public string escenaDestinoDespuesDeCinematica;
    private Coroutine currentCoroutine;

    [SerializeField] GameObject menuInGame;
    [SerializeField] GameObject menuPausa;
    [SerializeField] GameObject menuScript;
    [SerializeField] MenuPausa menuSc;
    //[SerializeField] GameObject plape;

    [SerializeField] List<int> enemigosMuertos = new List<int>();
    [SerializeField] List<int> cajasDestruidas = new List<int>();

    [SerializeField] List<int> objetosRecodigos = new List<int>();

    [SerializeField] public int currentSlot;
    private void Start()
    {

        InicializarLista();
    }
    public IEnumerator IniJuego()
    {
        yield return new WaitForSeconds(5f);
        IrACinematica(0, ConstantsGame.SCENEMAINMENU);
    }
    public void InicializarLista()
    {
        for (int i = 0; i < 14; i++)
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
    public void IrACinematica(int idCinematica, string escenaDestino)
    {
        idCinematicaActual = idCinematica;
        escenaDestinoDespuesDeCinematica = escenaDestino;
        loaderScene.LoadSceneString(ConstantsGame.CINEMATICA);
    }
    public void StartManagedCoroutine(IEnumerator coroutine)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(coroutine);
    }
    public void CancelarTransiciones()
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        StopAllCoroutines();
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

        menuSc.OcultarTodo();

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
        if (scene.name == ConstantsGame.SCENELOADINGSCREEN)
            return;

        ReferenciasPlayer();
        if(scene.name == "PantallaTiny")
        {
            StartCoroutine(IniJuego());
            return;
        }
        if (scene.name != "MainMenu" && gameData != null)
        {
            scoreFinal = gameData.scoreTotal;
            nEscena = gameData.lastSceneName;
        }
        if (scene.name == "MainMenu")
        {
            menuInGame.SetActive(true);
            Debug.Log("Estas en el Main Menu");
            return;
        }

        if (!loadingFromSave)
            return;

        // Configuración según la escena
        if (scene.name == "EscenaUno")
        {
            nEscena = "Puerto Alonso";
            menuInGame.SetActive(true);

            if (gameData == null)
            {
                return;
            }
            else
            {
                StartCoroutine(RestoreAfterLoad(gameData));
            }

            loadingFromSave = false;
            escenaActual = "EscenaUno";
            escenaSiguiente = "EscenaDos";
            menuInGame.SetActive(true);
            menuPausa.SetActive(false);
            
        }
        else if (scene.name == "EscenaDos")
        {
            menuInGame.SetActive(true);
            nEscena = "Riosinho";
            if (gameData == null)
            {
                return;
            }
            else
            {
                StartCoroutine(RestoreAfterLoad(gameData));
            }
            loadingFromSave = false;
            escenaActual = "EscenaDos";
            escenaSiguiente = "Creditos";
            menuInGame.SetActive(true);
            menuPausa.SetActive(false);
           
        }
    }

    
    public void SaveGame()
    {
        if (gameData == null)
        {
            gameData = new GameData(playerSalud, playerAtaque, playerController,
                currentLevel, SceneManager.GetActiveScene().name,
                currentSlot, scoreFinal, nEscena);
        }

        SaveLoadSystem.SaveGame(gameData, currentSlot);
        Debug.Log($"Juego guardado en slot {currentSlot}");
    }


    public void LoadGame(int slot)
    {
        GameData data = SaveLoadSystem.LoadGame(slot);
        if (data == null)
        {
            Debug.LogError("No hay datos en el slot");
            return;
        }

        currentSlot = slot;
        gameData = data;

        loadingFromSave = true;
        loaderScene.LoadSceneString(data.lastScene);
    }

    private IEnumerator RestoreAfterLoad(GameData data)

    {
        Debug.Log("RESTORE AFTER LOAD EJECUTADO");

        while (playerController == null || playerSalud == null || playerAtaque == null)
        {
            ReferenciasPlayer();
            yield return null;
        }


        yield return new WaitForEndOfFrame();

        Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();

        playerController.enabled = false;

        Vector2 cpPos = new Vector2(data.position[0], data.position[1]);
        //yield return new WaitForFixedUpdate();
        Debug.Log($"Moviendo jugador a: {data.position[0]}, {data.position[1]}");

        rb.simulated = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        //rb.simulated = true;
        rb.transform.position = cpPos;
        rb.simulated = true;


        playerSalud.vidasJugador = data.vidasJugador;
        playerSalud.vidasEXtras = data.vidasExtras;
        playerAtaque.cantidadBalas = data.balas;
        playerAtaque.seleccionArma = data.tipoArma;

        if (HUDManager.instancia != null)
        {
            HUDManager.instancia.ActualizarVida(playerSalud.vidasJugador);
            HUDManager.instancia.ActualizarArmadura(playerSalud.vidasEXtras);

            HUDManager.instancia.ActualizarBalasActual(playerAtaque.cantidadBalas);
            HUDManager.instancia.ActualizarArma(playerAtaque.seleccionArma);
        }

        yield return new WaitForFixedUpdate();

        playerController.enabled = true;
        AplicarEstadoNivel(data);
        Debug.Log("SPAWN CORRECTO EN CHECKPOINT: " + cpPos);
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
        IrACinematica(1, "EscenaUno");
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
        CancelarTransiciones();

        SoundEvents.DetenerMusica?.Invoke();
        loaderScene.LoadSceneString(ConstantsGame.SCENEMAINMENU);

        if (HUDManager.instancia != null)
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
    public void GuardarDesdeCheckpoint(Vector2 posicion)
    {
        if (playerController == null)
            ReferenciasPlayer();

        gameData = new GameData(
            playerSalud,
            playerAtaque,
            playerController,
            currentLevel,
            SceneManager.GetActiveScene().name,
            currentSlot,
            scoreFinal,
            nEscena
        );
        gameData.position[0] = posicion.x;
        gameData.position[1] = posicion.y;
        gameData.cajasDestruidas = cajasDestruidas;
        gameData.enemigosMuertos = enemigosMuertos;
        gameData.objetosRecogidos = objetosRecodigos;
   

        SaveLoadSystem.SaveGame(gameData, currentSlot);
        Debug.Log("Checkpoint guardado en: " + posicion);
    }
    public void RegistrarCajaDestruida(int id)
    {
        if (!cajasDestruidas.Contains(id))
            cajasDestruidas.Add(id);
    }
    public void RegistrarEnemigoMuerto(int id)
    {
        Debug.Log("Registrando enemigo: " + id);
        enemigosMuertos.Add(id);
    }

    public void ObjetosRecogidos(int id)
    {
        if(!objetosRecodigos.Contains(id))
            objetosRecodigos.Add(id);
    }
    public void AplicarEstadoNivel(GameData gameData)
    {
        foreach (ObjetoDestruible obj in FindObjectsOfType<ObjetoDestruible>())
        {
            if (gameData.cajasDestruidas.Contains(obj.idObj))
                Destroy(obj.gameObject);
        }

        foreach (Enemigo_IA enemigo in FindObjectsOfType<Enemigo_IA>())
        {
            if (gameData.enemigosMuertos.Contains(enemigo.idEn))
                Destroy(enemigo.gameObject);
        }

        foreach(ObjetoRecogible obj in FindObjectsOfType<ObjetoRecogible>())
        {
            if (gameData.objetosRecogidos.Contains(obj.objId))
                Destroy(obj.gameObject);
        }

    }

}