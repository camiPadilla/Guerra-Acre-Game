using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaquePersonaje : MonoBehaviour
{
    [SerializeField] GameObject prefabPiedra;
    [SerializeField] Queue<Piedra> piedraCola = new Queue<Piedra>();
    [SerializeField] int cantidadPiedras;
    [SerializeField] float fuerzatiro;
    [SerializeField] float fuerzaMaxima;
    [SerializeField] public Transform origen;
    public int direccion; 
    [SerializeField] GameObject miAtaque;
    [SerializeField] int seleccionArma;
    [SerializeField] Animator miAnimator;
    bool enAccion;
    // Start is called before the first frame update
    void Start()
    {
        InstanciarPiedras();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(direccion);
        switch (seleccionArma)
        {
            case 0:
                EntradaMelee();
                break; 
            case 1:
                EntradaPedra();
                break; 
            case 2:
                
                break;
        }

    }
    public void SetDireccion(int Ndir)
    {
        direccion = Ndir;

    }
    private void TirarPiedra()
    {
        Piedra piedraActual = piedraCola.Dequeue();
        Vector3 puntoIncial = new Vector3(transform.position.x,transform.position.y + 1,transform.position.z);
        piedraActual.Reposicionar(puntoIncial);
        piedraActual.Activar();
        piedraActual.Impulso(fuerzatiro, direccion);
        enAccion = false;
    }
    void InstanciarPiedras()
    {
        //piedraCola.Clear();
        while (piedraCola.Count < cantidadPiedras)
        {
            GameObject objeto = Instantiate(prefabPiedra, transform.position, Quaternion.identity);
            Piedra piedraActual = objeto.GetComponent<Piedra>();
            piedraActual.Instanciar(this);
            piedraCola.Enqueue(piedraActual);
            Debug.Log(piedraCola);
        }
    }
    public void GuardarEnCola(Piedra piedra)
    {
        piedraCola.Enqueue(piedra);
    }
    private void EntradaMelee()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AtaqueMachete();
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
            //Disparo
        }
    }
    private void AtaqueMachete()
    {
        miAtaque.transform.position = new Vector3(transform.position.x+0.5f*direccion,transform.position.y,transform.position.z);
        miAnimator.SetTrigger("atacar");
    }
    public void ActivarMachete()
    {
        miAtaque.SetActive(true);
        enAccion = true;
    }
    public void DesactivarMachete()
    {
        miAtaque.SetActive(false);
        enAccion = false;
    }
    public bool GetAccion()
    {
        return enAccion;
    }
}
