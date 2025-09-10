using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaquePersonaje : MonoBehaviour
{

    [SerializeField] GameObject prefabPiedra;
    [SerializeField] Queue<Piedra> piedraCola = new Queue<Piedra>();
    [SerializeField] int cantidadPiedras;
    [SerializeField] public float fuerzatiro;
    [SerializeField] public float fuerzaMaxima;
    [SerializeField] public Transform origen;
    public int direccion;
    // Start is called before the first frame update
    void Start()
    {

        InstanciarPiedras();
    }

    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log(direccion);

    }
    public void SetDireccion(int Ndir)
    {
        direccion = Ndir;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            fuerzatiro = 0;
            SoundEvents.CargarFuerzaPiedra?.Invoke();
        }


        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (fuerzatiro <= fuerzaMaxima)
            {
                fuerzatiro = fuerzatiro + fuerzaMaxima * Time.deltaTime;               
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {            
            TirarPiedra();
            SoundEvents.DetenerCarga?.Invoke();
        }
    }
    void TirarPiedra()
    {
        Piedra piedraActual = piedraCola.Dequeue();
        Vector3 puntoIncial = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        piedraActual.Reposicionar(puntoIncial);
        piedraActual.Activar();
        piedraActual.Impulso(fuerzatiro, direccion);
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
}
