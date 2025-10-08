using System.Collections.Generic;

//[CreateAssetMenu(fileName = "Progreso", menuName = "Progreso/DatosProgreso")]

[System.Serializable]
public class ProgressData/* : ScriptableObject*/
{
    public int currentWorld = 0;
    public int lastVisitedWorld = 0;

    public int correctTotal;
    public int incorrectTotal;
    public List<WorldCompletionData> worldData;

    public ProgressData(int currentWorld, int lastVisitedWorld, List<WorldCompletionData> worldData, int correctTotal, int incorrectTotal)
    {
        this.currentWorld = currentWorld;
        this.lastVisitedWorld = lastVisitedWorld;
        this.worldData = worldData;

        this.correctTotal = correctTotal;
        this.incorrectTotal = incorrectTotal;
    }
}


[System.Serializable]
public class WorldCompletionData{
    public int worldIndex;
    public bool worldCompleted;
    public int lastVisitedLevel = 0;
    public List<LevelCompletionData> levelData;
    public List<NivelesConversacion> nivelesConversaciones;

    public WorldCompletionData(int worldIndex, bool worldCompleted, int lastVisitedLevel, List<LevelCompletionData> levelData, List<NivelesConversacion> nivelesConversaciones)
    {
        this.worldIndex = worldIndex;
        this.worldCompleted = worldCompleted;
        this.lastVisitedLevel = lastVisitedLevel;
        this.levelData = levelData;
        this.nivelesConversaciones = nivelesConversaciones;
    }
}

[System.Serializable]
public class LevelCompletionData{
    public int levelIndex;
    public bool levelCompleted;
    public int levelMaxScore;

    public LevelCompletionData(int levelIndex, bool levelCompleted, int levelMaxScore)
    {
        this.levelIndex = levelIndex;
        this.levelCompleted = levelCompleted;
        this.levelMaxScore = levelMaxScore;
    }
}

[System.Serializable]
public class NivelesConversacion
{
    public int nivelesConversacion;
    public bool nivelConversacionCompletado;

    public NivelesConversacion(int nivelesCon, bool nivelConComp)
    {
        nivelesConversacion = nivelesCon;
        nivelConversacionCompletado = nivelConComp;
    }
}

