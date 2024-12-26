using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum WallType {
    Blue,
    Red,
}

public class StageManager : MonoBehaviour
{
    public WallType solidWall = WallType.Blue;
    private List<Wall> subscribedWalls = new();

    public Wave[] waves;
    private int index = 0;
    private float currentTime;

    public static StageManager getInstance() {
        return FindFirstObjectByType<StageManager>();
    }

    void Start() {
        InstatiateWave();         
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            foreach (Wall wall in subscribedWalls)
            {
                wall.ChangeState();
                solidWall = solidWall == WallType.Blue ? WallType.Red : WallType.Blue;
            }
        }

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

    public void AddWall(Wall wall) {
        subscribedWalls.Add(wall);
    }

    public void RemoveWall(Wall wall) {
        subscribedWalls.Remove(wall);
    }
}
