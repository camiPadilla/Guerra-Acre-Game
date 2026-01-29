using UnityEngine;

public class CheckPoints : ObjetoRecogible
{
    public int indexCP;
    private bool checkPointActivo;
    public GameObject pantallaGuardado;

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

    public void Guardar()
    {
        pantallaGuardado.SetActive(true);
        GameManager.instancia.CambiarDeEstado(1);
    }

    public void SalieGuardar()
    {
        pantallaGuardado.SetActive(false);
        GameManager.instancia.CerrarEstado();
    }
}
