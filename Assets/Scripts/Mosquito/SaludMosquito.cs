using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaludMosquito : MonoBehaviour
{
    [SerializeField] int vidas;
    // Start is called before the first frame update
    public void perderVida()
    {
        vidas--;
        Debug.Log("Perdi una vida mosquito");
        if(vidas == 0)
        {
            SoundEvents.MorirMosquito?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
