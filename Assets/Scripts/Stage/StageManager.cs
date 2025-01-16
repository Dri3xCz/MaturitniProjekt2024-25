using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum WallType {
    Blue,
    Red,
}

public class StageManager : MonoBehaviour
{

    public GameObject menuCanvas; 
    private bool isPaused = false;
    public UIBehaviour scoreTextBehaviour;
    private TextMeshProUGUI scoreTextComponent;
    public UIBehaviour multiplayerTextBehaviour;
    private TextMeshProUGUI multiplayerTextComponent;


    public WallType solidWall = WallType.Blue;
    private List<Wall> subscribedWalls = new();

    public Wave[] waves;

    public int score;
    public int multiplayer;
    private float scoreTickTime = 1;

    private int index = 0;
    private float currentTime;

    public static StageManager getInstance() {
        return FindFirstObjectByType<StageManager>();
    }

    void Start() {
        scoreTextComponent = scoreTextBehaviour.GetComponent<TextMeshProUGUI>();
        multiplayerTextComponent = multiplayerTextBehaviour.GetComponent<TextMeshProUGUI>();
        InstatiateWave();    
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        scoreTickTime -= Time.deltaTime;
        if (scoreTickTime <= 0) {
            score += 100 * multiplayer;
            scoreTickTime = 1;
            scoreTextComponent.text = score.ToString();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !isPaused) {
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
        multiplayer++;
        multiplayerTextComponent.text = multiplayer.ToString();
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

    public void UpdateUI() {
        scoreTextComponent.text = score.ToString();
        multiplayerTextComponent.text = multiplayer.ToString();
    }

    public void PauseGame()
    {
        menuCanvas.SetActive(true); 
        Time.timeScale = 0f;        
        isPaused = true;
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;         
        isPaused = false;
        AudioListener.pause = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}