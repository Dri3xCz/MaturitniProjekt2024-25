using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum WallType {
    White,
    Red,
}

public class StageManager : MonoBehaviour
{
    public Canvas menuCanvas; 
    public Canvas settingsCanvas; 
    private bool isPaused = false;
    private bool isPausedFromOutside = false;
    public UIBehaviour scoreTextBehaviour;
    private TextMeshProUGUI scoreTextComponent;
    public UIBehaviour multiplayerTextBehaviour;
    private TextMeshProUGUI multiplayerTextComponent;


    public WallType solidWall = WallType.Red;
    private readonly List<Wall> subscribedWalls = new();

    public Wave[] waves; 

    public int score;
    public int multiplayer;
    private float scoreTickTime = 1;

    private int index = 0;
    private float currentTime;

    private CameraShake cameraShake;

    public static StageManager getInstance() {
        return FindFirstObjectByType<StageManager>();
    }

    void Start() {
        scoreTextComponent = scoreTextBehaviour.GetComponent<TextMeshProUGUI>();
        multiplayerTextComponent = multiplayerTextBehaviour.GetComponent<TextMeshProUGUI>();
        cameraShake = CameraShake.getInstance();
        InstatiateWave();    
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && isPausedFromOutside == false)
        {
            (isPaused ? (Action<bool>)ResumeGame : PauseGame)(true); 
        }

        scoreTickTime -= Time.deltaTime;
        if (scoreTickTime <= 0) {
            score += 100 * multiplayer;
            scoreTickTime = 1;
            scoreTextComponent.text = score.ToString();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && (!isPaused || isPausedFromOutside)) {
            foreach (Wall wall in subscribedWalls)
            {
                wall.ChangeState();
                solidWall = solidWall == WallType.White ? WallType.Red : WallType.White;
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
        foreach (SpawnableObject waveEnemy in waves[index].enemies) {
            Instantiate(
                waveEnemy.enemy,
                waveEnemy.position,
                transform.rotation
            );
        }

        currentTime = waves[index].nextWaveDelay;
        if (waves[index].shouldAwardMultiplier) {
            multiplayer++;
            multiplayerTextComponent.text = multiplayer.ToString();
        }
    }

    public void AddWall(Wall wall) {
        subscribedWalls.Add(wall);
    }

    public void RemoveWall(Wall wall) {
        subscribedWalls.Remove(wall);
    }

    public void UpdateUI() {
        scoreTextComponent.text = score.ToString();
        multiplayerTextComponent.text = multiplayer.ToString();
    }

    public void OnSettingsClicked() {
        menuCanvas.gameObject.SetActive(!menuCanvas.isActiveAndEnabled);
        settingsCanvas.gameObject.SetActive(!settingsCanvas.isActiveAndEnabled);
    }

    public void PauseGame(bool showMenu = false)
    {
        if (showMenu) {
            menuCanvas.gameObject.SetActive(true); 
        } else {
            isPausedFromOutside = true;
        }
        Time.timeScale = 0f;        
        isPaused = true;
        AudioListener.pause = true;
    }

    public void ResumeGame(bool showMenu = false)
    {
        if (showMenu) {
            menuCanvas.gameObject.SetActive(false);
        } else {
            isPausedFromOutside = false;
        }
        Time.timeScale = 1f;         
        isPaused = false;
        AudioListener.pause = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShakeCamera() {
        cameraShake.Shake();
    }
}