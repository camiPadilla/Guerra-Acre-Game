using System.Collections;
using UnityEngine;

namespace PantallaCarga
{
    public class PantallaDeCarga : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(CargarConRetraso());
        }

        IEnumerator CargarConRetraso()
        {
          
            yield return new WaitForSeconds(10f);
            print("Cambiare de Escene");
            LoaderScene.instance.CargarDestino();
        }
    }
}
