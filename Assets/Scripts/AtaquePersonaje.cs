using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaquePersonaje : MonoBehaviour
{
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
    public int dirX; 
    public float dirY;
    [SerializeField] Arma machete;
    [SerializeField] int seleccionArma;
    [SerializeField] Animator miAnimator;
    bool enAccion;

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
        if (Input.GetKeyDown(KeyCode.Alpha3)) seleccionArma = 2;


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

    }
    public void SetDireccion(int NdirX, float NdirY)
    {
        dirX = NdirX;
        dirY = NdirY;

    }
    private void TirarPiedra()
    {
        Proyectil piedraActual = piedraCola.Dequeue();
        Vector3 puntoIncial = new Vector3(transform.position.x,transform.position.y + 1,transform.position.z);
        piedraActual.Reposicionar(puntoIncial);
        piedraActual.ActivarProyectil();
        piedraActual.Impulso(fuerzatiro, dirX, dirY);
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
        }
    }
    private void EntradaPedra()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fuerzatiro = 0;
            enAccion = true;
        }
        if (Input.GetButton("Fire1"))
        {
            if (fuerzatiro <= fuerzaMaxima)
            {
                fuerzatiro = fuerzatiro + fuerzaMaxima * Time.deltaTime;
            }
        }
        if (Input.GetButtonUp("Fire1")) TirarPiedra();
    }
    private void EntradaDisparo()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }
    private void Disparar()
    {
        enAccion = true;
        Proyectil balaActual = balaCola.Dequeue();
        Vector3 puntoIncial = new Vector3(transform.position.x+dirX, transform.position.y + dirY, transform.position.z);
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
        //machete.Reposicionar(new Vector3(transform.position.x + 0.5f * dirX, transform.position.y, transform.position.z));
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
