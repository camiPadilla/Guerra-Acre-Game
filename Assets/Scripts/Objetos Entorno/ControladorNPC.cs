using UnityEngine;

public class ControladorNPC : ObjetoRecogible
{
    [SerializeField] DialogosSO dialogo;

    public void Interactuar()
    {
        if (GameManager.instancia.Onplaying())
        {
            MostrarMensaje();
        }
    }

    public void MostrarMensaje()
    {
        SoundEvents.HablarAliadoNPC?.Invoke();
        HUDManager.instancia.IniciarDialogo(dialogo);
    }
}