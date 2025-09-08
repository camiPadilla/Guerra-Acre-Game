using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;



public class AtaquePersonaje : MonoBehaviour
{
    //Linea para referenciar sonido
    [SerializeField] private StudioEventEmitter emitter;
    

    [SerializeField] GameObject prefabPiedra;
    [SerializeField] Queue<Piedra> piedraCola = new Queue<Piedra>();
    [SerializeField] int cantidadPiedras;
    [SerializeField] float fuerzatiro;
    [SerializeField] float fuerzaMaxima;
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
        StartCoroutine(PanoramizarFuerzadeCarga());
        //Debug.Log(direccion);

    }
    public void SetDireccion(int Ndir)
    {
        direccion = Ndir;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            fuerzatiro = 0;
            GetComponent<StudioEventEmitter>().Play();
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
            GetComponent<StudioEventEmitter>().Stop();
            TirarPiedra();
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

    public IEnumerator PanoramizarFuerzadeCarga()
    {
        if (emitter != null && emitter.EventInstance.isValid())
        {
            float paneoValor;
            if(direccion == -1) emitter.EventInstance.setParameterByName("Paner", 1);

            else if(direccion == 1) emitter.EventInstance.setParameterByName("Paner", -1);

            emitter.EventInstance.getParameterByName("Paner", out paneoValor);

            Debug.Log("Valor actual de Paneo (deg): " + paneoValor);
            Debug.Log("Valor actual de Direccion: " + direccion);

            while (fuerzatiro < fuerzaMaxima)
            {
                // Normalizamos la fuerza entre 0 y 1
                float progreso = Mathf.Clamp01(fuerzatiro / fuerzaMaxima);

                // Interpolamos el paneo hacia 0
                float paneoInterpolado = Mathf.Lerp(paneoValor, 0f, progreso);

                // Enviamos a FMOD
                emitter.EventInstance.setParameterByName("Paneo", paneoInterpolado);

                // Debug
                Debug.Log("Paneo: " + paneoInterpolado + " | Fuerza: " + fuerzatiro);

                // Espera un frame antes de seguir
                yield return null;
            }
        }
    }

}
