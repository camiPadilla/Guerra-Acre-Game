using UnityEngine;

[CreateAssetMenu(fileName = "nuevaNota", menuName = "SO/Nota")]
public class NotasSO : ScriptableObject
{
    [TextArea(3, 8)]
    public string textoNota;
    public string ID;

}
