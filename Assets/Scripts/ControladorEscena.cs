using PantallaCarga;
using System.Collections;
using TarodevController;
using TMPro;
using UnityEngine;

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
    [SerializeField] NotasSO notaNueva;

    [Header("UI")]
    //[SerializeField] GameObject MenuInGame;
    [SerializeField] GameObject HUD;
    [SerializeField] HUDManager hudManager;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI textObj;

    [Header("UI Textos pantalla fin")]
    public TMP_Text textVidas;
    public TMP_Text textNot;
    public TMP_Text textBalas;
    public TMP_Text textEnemigos;
    public TMP_Text textScoreFinal;

    [Header("UI Textos pantalla muerte")]
    public TMP_Text textNotM;
    public TMP_Text textBalasM;
    public TMP_Text textEnemigosM;
    [SerializeField] TMP_Text nomNivel;
    [SerializeField] TMP_Text nivel;

    [Header("Checkpoints")]
    public Vector2 ChPoint;

    [Header("Score")]
    public int scVidas;
    public int scNotas;
    public int scEnem;
    public int scFinalLvl;

public void Awake()
    {
        if (loaderScene == null) loaderScene = FindObjectOfType<LoaderScene>();
         if (masterGameManager == null) masterGameManager = FindObjectOfType<MasterGameManager>();
    }
    private void Start()
    {

       
       // if (MenuInGame == null) MenuInGame = GameObject.FindWithTag("canvas");
        if (HUD != null) HUD.SetActive(true);
        


        switch (sceneIndex)
        {
            case 0:
                nameScene = ConstantsGame.SCENEMAINMENU;
                break;
            case 1:
                nameScene = ConstantsGame.SCENAUNO;
                textObj.text = "Objetivo: Ve al campamento y recoge las notas";
                hudManager.Bienvenido();
                nomNivel.text = "Puerto Alonso";
                nivel.text = "Nivel 1";
                break;
            case 2:
                nameScene = ConstantsGame.SCENADOS;
                textObj.text = "Abrete paso y reunete con tus compañeros";
                hudManager.Bienvenido();
                nomNivel.text = "Riosinho";
                nivel.text = "Nivel 2";
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
        //if (vidas != null) HUDManager.instancia.ActualizarVida(vidas.vidasJugador);
        if (vidas != null) 
        { 
            textVidas.text = vidas.vidasJugador.ToString();
        }
        if (cantidadNotas != null)
        { 
            textNot.text = cantidadNotas.cantNotas.ToString();
            textNotM.text = cantidadNotas.cantNotas.ToString();
        }
        if (balas != null) 
        {
            textBalasM.text = balas.GettotalBalas().ToString();
            textBalas.text = balas.GettotalBalas().ToString();
        }
        if (gameManager != null) 
        { 
            textEnemigosM.text = gameManager.enemigosMuertos.ToString();
            textEnemigos.text = gameManager.enemigosMuertos.ToString();
        }
        
    }

    public void SiguienteNivel()
    {
        if (HUD != null) HUD.SetActive(false);

        gameManager.CambiarDeEstado(0);
        SoundEvents.DetenerMusica?.Invoke();

        switch (sceneIndex)
        {
            case 1:

                MasterGameManager.instance.IrACinematica(2, ConstantsGame.SCENADOS);
                break;

            case 2:
  
                MasterGameManager.instance.IrACinematica(3, ConstantsGame.SCENECREDITS);
                break;
        }
    }
    public void VolverMenu()
    {
        masterGameManager.IrMenu();
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

    public void ObtenerNota(NotasSO nota)
    {
        notaNueva = nota;
        masterGameManager.AddNota(notaNueva);
    }
    public void CalcularScoreFinal()
    {
        scVidas = vidas.vidasJugador * 150;
        scNotas = cantidadNotas.cantNotas * 250;
        scEnem = gameManager.enemigosMuertos * 100;

        scFinalLvl = scVidas + scNotas + scEnem;

        textScoreFinal.text = scFinalLvl.ToString();

        masterGameManager.RecibirScoreFinal(scFinalLvl);
        Debug.Log("Score final calculado: " + scFinalLvl);
    }
    public void Guardar()
    {
        CalcularScoreFinal();
        MasterGameManager.instance.GuardarDesdeCheckpoint(ChPoint);
        StartCoroutine(GuarEx());
    }
    public IEnumerator GuarEx()
    {
        float duration = 0.5f;
        float t = 0f;

     
        while (t < duration)
        {
            text2.alpha = Mathf.Lerp(0f, 1f, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        text2.alpha = 1f;

        yield return new WaitForSeconds(2f);

      
        t = 0f;
        while (t < duration)
        {
            text2.alpha = Mathf.Lerp(1f, 0f, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        text2.alpha = 0f;
    }


}
