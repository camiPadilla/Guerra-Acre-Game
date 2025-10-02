using UnityEngine;

[CreateAssetMenu(fileName = "Rhythm", menuName = "SO/Rhythm")]
public class RhythmSO : ScriptableObject
{
    public float figura;
    // 3.13f 1.6f para llegar al corazon
    //para medio 1.059 
    // para tiempo rapido 0.63
    public float tiempo = 0.5825f;
    public float distancia = 9.5f;
}
