using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : ObjetoRecogible
{
    public int indexCP;
    private bool checkPointActivo;
    private Animator animator;
    public GameObject pantallaGuardado;

    private bool checkPointSound = true;

    void Start()
    {
        animator = GetComponent<Animator>();
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
                SoundEvents.CheckpointActivado?.Invoke(); // Sonido by Chelo :D
                checkPointSound = false;
            }
        }else
        {
            checkPointSound = true;
        }
    }
    public bool Verificar(CheckPoints anterior)
    {
        if (indexCP == anterior.indexCP)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void Guardar()
    {
        pantallaGuardado.SetActive(true);
    }
    public void SalieGuardar()
    {
        pantallaGuardado.SetActive(false);
    }
}
