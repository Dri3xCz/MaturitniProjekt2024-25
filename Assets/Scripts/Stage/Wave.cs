[System.Serializable]
public class Wave
{
    public SpawnableObject[] enemies;
    public float nextWaveDelay;
    public bool shouldAwardMultiplier = true;
}