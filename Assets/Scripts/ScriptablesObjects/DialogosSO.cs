using UnityEngine;

[CreateAssetMenu(fileName = "nuevoDialogo", menuName  = "SO/dialogo")]
public class DialogosSO : ScriptableObject
{
    [TextArea(3,8)]
    public string texto;
  
}
