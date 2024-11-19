using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Wave[] waves;
    private int index = 0;
    private float currentTime;

    void Start() {
        InstatiateWave();         
    }

    void Update() {
        if (currentTime >= 0) {
            currentTime -= Time.deltaTime;
            return;
        }

        if (index >= waves.Length - 1) {
            // Properly endstage here
            return;
        }

        index++;
        InstatiateWave();
    }

    private void InstatiateWave() {
        foreach (WaveEnemy waveEnemy in waves[index].enemies) {
            Instantiate(
                waveEnemy.enemy,
                waveEnemy.position,
                transform.rotation
            );
        }

        currentTime = waves[index].nextWaveDelay;
    }
}
