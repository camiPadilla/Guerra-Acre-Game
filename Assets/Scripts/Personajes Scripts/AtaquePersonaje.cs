using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaquePersonaje : MonoBehaviour
{
    [SerializeField] LineRenderer trayectoria;
    [SerializeField] GameObject prefabPiedra;
    [SerializeField] GameObject prefabBala;
    [SerializeField] Queue<Proyectil> piedraCola = new Queue<Proyectil>();
    [SerializeField] Queue<Proyectil> balaCola = new Queue<Proyectil>();
    [SerializeField] int cantidadPiedras;
    [SerializeField] int cantidadBalas;
    [SerializeField] public float fuerzaDisparo;
    [SerializeField] public float fuerzatiro;
    [SerializeField] public float fuerzaMaxima;
    [SerializeField] public Transform origen;
    [SerializeField] Rigidbody2D miRigid;
    public int dirX;
    public float dirY;
    [SerializeField] Arma machete;
    [SerializeField] int seleccionArma;
    [SerializeField] int balasActual = 0;
    bool enAccion;
    bool recargando;
    bool conArma=false;
    // Start is called before the first frame update
    void Start()
    {
        InstanciarProyectiles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) seleccionArma = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) seleccionArma = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && conArma) seleccionArma = 2;
        HUDManager.instancia.ActualizarArma(seleccionArma);
        if (Input.GetAxis("Horizontal") >= 0.1f)
        {
            dirX = 1;
        }
        else if(Input.GetAxis("Horizontal") <= -0.1f)
        {
            dirX = -1;
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            dirY = Input.GetAxis("Vertical");
        } else dirY = 0;
        if (enAccion)
        {
            miRigid.velocity = Vector2.zero;
        }

        //Debug.Log(dirX);
        switch (seleccionArma)
        {
            case 0:
                EntradaMelee();
                break; 
            case 1:
                EntradaPedra();
                break; 
            case 2:
                EntradaDisparo();
                break;
        }
        if (Input.GetKeyDown(KeyCode.R) && seleccionArma == 2)
        {
            Recargar();
        }

    }
    public void ObtenerArma()
    {
        conArma = true;
        HUDManager.instancia.ActivarRifle();
    }
    public int GetBalasActuales()
    {
        return balasActual;
    }
    public void SetDireccion(int NdirX, float NdirY)
    {
        dirX = NdirX;
        dirY = NdirY;

    }
    private void TirarPiedra()
    {
        Proyectil piedraActual = piedraCola.Dequeue();
        Vector3 puntoIncial = new Vector3(transform.position.x,transform.position.y + 2,transform.position.z);
        piedraActual.Reposicionar(puntoIncial);
        
        piedraActual.ActivarProyectil();
        piedraActual.Impulso(fuerzatiro, dirX, dirY);
        SoundEvents.LanzarPiedra?.Invoke(); //Sound By Chelo :D
        SoundEvents.DetenerCarga?.Invoke(); //Sound By Chelo :D
        enAccion = false;
    }
    
    void InstanciarProyectiles()
    {
        //piedraCola.Clear();
        while (piedraCola.Count < cantidadPiedras)
        {
            GameObject objeto = Instantiate(prefabPiedra, transform.position, Quaternion.identity);
            Proyectil piedraActual = objeto.GetComponent<Proyectil>();
            piedraActual.Instanciar(this);
            piedraCola.Enqueue(piedraActual);
        }
        //Balas
        while (balaCola.Count < cantidadBalas)
        {
            GameObject objeto = Instantiate(prefabBala, transform.position, Quaternion.identity);
            Proyectil balaActual = objeto.GetComponent<Proyectil>();
            balaActual.Instanciar(this);
            balaCola.Enqueue(balaActual);
        }
    }
    public void GuardarEnCola(Proyectil proyectil, int tipo)
    {
        if (tipo == 0) piedraCola.Enqueue(proyectil);
        if (tipo == 1) balaCola.Enqueue(proyectil);
    }
    private void EntradaMelee()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(AtaqueMachete());
            SoundEvents.AtaqueMachete?.Invoke(); //Sound By Chelo :D
        }
    }
    private void EntradaPedra()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fuerzatiro = 0;
            enAccion = true;
            SoundEvents.CargarFuerzaPiedra?.Invoke(); //Sound By Chelo :D
        }
        if (Input.GetButton("Fire1"))
        {
            if (fuerzatiro <= fuerzaMaxima)
            {
                fuerzatiro = fuerzatiro + fuerzaMaxima * Time.deltaTime;
            }
        }
        Vector3 puntoIncial = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        
        if (Input.GetButtonUp("Fire1")) TirarPiedra();
    }
    private void EntradaDisparo()
    {
        if (Input.GetButtonDown("Fire1") && recargando == false)
        {
            if (balasActual == 0)
            {
                Recargar();
            }
            if (balasActual > 0)
            {
                Disparar();
                balasActual--;
                HUDManager.instancia.ActualizarBalasActual(balasActual);
                StartCoroutine(nameof(TiempoRecarga), 1f);
            }


        }
    }
    private void Recargar()
    {

        Debug.Log("recargado");
        int totalBalas = GetComponent<InventarioManager>().GetBalas();
        if (totalBalas > 0)
        {
            StartCoroutine(nameof(TiempoRecarga), 2);
            totalBalas -= (5 - balasActual);
            if (totalBalas < 0)
            {
                balasActual = (5 + cantidadBalas);
                totalBalas = 0;

            }
            else
            {
                balasActual = 5;

            }
            SendMessage("SetBalas", totalBalas);
            Debug.Log("tines en tu cargador " + balasActual);

        }
        else
        {
            Debug.Log("no puedes recargar no tienes balas ");
        }
        HUDManager.instancia.ActualizarBalasActual(balasActual);

    }
    IEnumerator TiempoRecarga(float espera)
    {
        recargando = true;
        Debug.Log("Esta recargando");
        yield return new WaitForSeconds(espera);
        recargando = false;
        Debug.Log("Ya recargo");

    }
    private void Disparar()
    {
        enAccion = true;
        Proyectil balaActual = balaCola.Dequeue();
        Vector3 puntoIncial = new Vector3(transform.position.x+dirX, transform.position.y + dirY+1, transform.position.z);
        balaActual.Reposicionar(puntoIncial);
        balaActual.ActivarProyectil();
        balaActual.Impulso(fuerzaDisparo, dirX, dirY);
        enAccion = false;
    }
    public void EndDiapro()
    {
        enAccion = true;
    }
    private IEnumerator AtaqueMachete()
    {
        machete.Reposicionar(new Vector3(transform.position.x + 0.592f * dirX, transform.position.y+ 0.967f, transform.position.z));
        ActivarMachete();
        yield return new WaitForSeconds(.5f);
        DesactivarMachete();
        //miAnimator.SetTrigger("atacar");
    }
    public void ActivarMachete()
    {
        machete.Activar();
        enAccion = true;
    }
    public void DesactivarMachete()
    {
        machete.Desactivar();
        enAccion = false;
    }
    public bool GetAccion()
    {
        return enAccion;
    }
}
