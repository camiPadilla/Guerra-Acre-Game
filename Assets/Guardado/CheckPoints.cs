using Unity.VisualScripting;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int indexCP;
    private bool checkPointActivo;
    public GameObject pantallaGuardado;
    [SerializeField]private Animator animator;
    public ControladorEscena controladorEscena;
    public GameObject ImE;

    private bool checkPointSound = true;

    void Start()
    {
        checkPointActivo = false;
    }

    public void CambiarEstadoBandera()
    {
        checkPointActivo = !checkPointActivo;
        animator.SetBool("usado", checkPointActivo);
        if (checkPointActivo)
        {
            if (checkPointSound)
            {
                SoundEvents.CheckpointActivado?.Invoke();
                checkPointSound = false;
            }
        }
        else
        {
            checkPointSound = true;
        }
    }

    public bool Verificar(CheckPoints anterior)
    {
        return indexCP != anterior.indexCP;
    }

    public void GuardarUI()
    {
        pantallaGuardado.SetActive(true);
        GameManager.instancia.CambiarDeEstado(1);
    }

    public void SalieGuardar()
    {
        pantallaGuardado.SetActive(false);
        GameManager.instancia.CerrarEstado();
        Time.timeScale = 1;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        controladorEscena.ChPoint = transform.position;
        ImE.SetActive(true);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        ImE.SetActive(false);
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&other.gameObject.GetComponent<InputPlayer>().getInteractuable())
        {
            Time.timeScale = 0;
            GuardarUI();
        }
    }
}
