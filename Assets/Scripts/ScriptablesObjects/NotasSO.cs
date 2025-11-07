using UnityEngine;

[CreateAssetMenu(fileName = "nuevaNota", menuName = "SO/Nota")]
public class NotasSO : ScriptableObject
{
    [TextArea(3, 8)]
    public string textoNota;
    public string ID;
    public int numeroNota;
    public Sprite notaImagen;
    public bool obtenida;

    public void SetObtenida(bool estado)
    {
        obtenida = estado;
    }   
}