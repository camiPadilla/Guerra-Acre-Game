using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class AtaquePersonaje : MonoBehaviour
{
    [SerializeField] LineRenderer trayectoria;
    [SerializeField] GameObject prefabPiedra;
    [SerializeField] GameObject prefabBala;
    [SerializeField] Queue<Proyectil> piedraCola = new Queue<Proyectil>();
    [SerializeField] Queue<Proyectil> balaCola = new Queue<Proyectil>();
    [SerializeField] int cantidadPiedras;
    [SerializeField] public int cantidadBalas;
    [SerializeField] public float fuerzaDisparo;
    [SerializeField] public float fuerzatiro;
    [SerializeField] public float fuerzaMaxima;
    [SerializeField] public Transform origen;
    [SerializeField] Rigidbody2D miRigid;
    public int dirX;
    public float dirY;
    [SerializeField] Arma machete;
    [SerializeField] public int seleccionArma = 0;
    [SerializeField] int balasActual = 0;
    bool enAccion;
    bool recargando;
    bool conArma=false;

    [SerializeField] private PlayerAnimator animator;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Transform puntoTiro;
    private bool recibirAltura;
    private SpriteManager miSprite;
    // Start is called before the first frame update
    void Start()
    {
        InstanciarProyectiles();
        _player = GetComponent<PlayerController>();
        miSprite = GetComponent<SpriteManager>();
        miSprite.CambiarArma(seleccionArma);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SetArma(0); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { SetArma(1); }
                if (Input.GetKeyDown(KeyCode.Alpha3) && conArma) { SetArma(2); }
        HUDManager.instancia.ActualizarArma(seleccionArma);
        
        if (_player._frameInput.Move.x >= 0.1f)
        {
            dirX = 1;
        }
        else if(_player._frameInput.Move.x <= -0.1f)
        {
            dirX = -1;
        }
        if (recibirAltura)
        {
            dirY = _player.GetdirY();
            animator.DirY(dirY);
        }
        if (_player.GetForzarAgachado()) { dirY = -1; animator.DirY(dirY); }
        else
        {
            if (dirY > 0.4f) dirY = 1;
            else if (dirY < -0.4f)
            {
                dirY = -1;
                _player.SetAgachado(true);
            }
        }
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
    public int GettotalBalas()
    {
        int total;
        total = balasActual + GetComponent<InventarioManager>().GetBalas();

        return total;
    }

    private void TirarPiedra()
    {
        Proyectil piedraActual = piedraCola.Dequeue();
        Vector3 puntoIncial;// = new Vector3(transform.position.x,transform.position.y + 2,transform.position.z);
        puntoIncial = puntoTiro.position;
        piedraActual.Reposicionar(puntoIncial);
        
        piedraActual.ActivarProyectil();
        piedraActual.Impulso(fuerzatiro, dirX, dirY);
        SoundEvents.LanzarPiedra?.Invoke(); //Sound By Chelo :D
        SoundEvents.DetenerCarga?.Invoke(); //Sound By Chelo :D
        //enAccion = false;
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
            //StartCoroutine(AtaqueMachete());
            _player.Detener();
            IniciaAccion();
            animator.AtaqueMacheteAn();
            SoundEvents.AtaqueMachete?.Invoke(); //Sound By Chelo :D
        }
    }
    private void EntradaPedra()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fuerzatiro = 0;
            //enAccion = true;
            IniciaAccion();
            _player.Detener();
            animator.AtaquePiedra();
            SoundEvents.CargarFuerzaPiedra?.Invoke(); //Sound By Chelo :D
        }
        if (Input.GetButton("Fire1"))
        {
            if (fuerzatiro <= fuerzaMaxima)
            {
                fuerzatiro = fuerzatiro + fuerzaMaxima * Time.deltaTime;
                float fuerzaRel = ((fuerzatiro / fuerzaMaxima)*2 - 1);
                //Debug.Log(fuerzaRel);
                animator.FuerzaY(fuerzaRel);
            }
        }
        //Vector3 puntoIncial = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        
        if (Input.GetButtonUp("Fire1"))
        {
            animator.TiraPiedra();
            recibirAltura = false;
            //TirarPiedra();
        }
    }
    private void EntradaDisparo()
    {
        if (Input.GetButtonDown("Fire1") && recargando == false)
        {
            
            if (balasActual > 0)
            {
                SoundEvents.DisparoEnemigo?.Invoke(transform.position.x); //Sound By Chelo :D
                Disparar();
                balasActual--;
                HUDManager.instancia.ActualizarBalasActual(balasActual);
                StartCoroutine(nameof(TiempoRecarga), 1f);
            }
            else if (balasActual == 0)
            {
                Recargar();
            }

        }
    }
    private void Recargar()
    {

        Debug.Log("recargado");
        int totalBalas = GetComponent<InventarioManager>().GetBalas();
        if (totalBalas > 0)
        {
            SoundEvents.RecargarBalas.Invoke(); //Sound By Chelo :D
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
            SoundEvents.SinBalas?.Invoke(); //Sound By Chelo :D
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
        //machete.Reposicionar(new Vector3(transform.position.x + 0.592f * dirX, transform.position.y+ 0.967f, transform.position.z));
        //ActivarMachete();
        //animator.AtaqueMacheteAn();
        _player.IniciarDIalogo();
        yield return new WaitForSeconds(.5f);
        _player.TerminarDialogo();
        //DesactivarMachete();
        //miAnimator.SetTrigger("atacar");
    }
    //public void ActivarMachete()
    //{
    //    machete.Activar();
    //    enAccion = true;
    //}
    //public void DesactivarMachete()
    //{
    //    machete.Desactivar();
    //    enAccion = false;
    //}
    public bool GetAccion()
    {
        return enAccion;
    }
    public void SetArma(int nSel)
    {
        if (!enAccion)
        {
            seleccionArma = nSel;
            miSprite.CambiarArma(seleccionArma);
            animator.CambiarArmaAn(seleccionArma);
            switch (nSel)
            {
                case 0:
                    SoundEvents.CambiarArmaMachete?.Invoke();
                    break;
                case 1:
                    SoundEvents.CambiarArmaPiedra?.Invoke();
                    break;
                case 2:
                    SoundEvents.RecogerArma?.Invoke();
                    break;
            }
        }
    }
    public void IniciaAccion()
    {
        _player.IniciarDIalogo();
        enAccion = true;
    }
    public void TerminarAccion()
    {
        _player.TerminarDialogo();
        recibirAltura = true;
        _player.SetAgachado(false);
        //StartCoroutine(Retraso(0.1f));
        enAccion = false;
    }
    public IEnumerator Retraso(float sec)
    {
        yield return new WaitForSeconds(sec);
        _player.SetAgachado(false);

    }
}
