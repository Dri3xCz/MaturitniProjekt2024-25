using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum WallType {
    White,
    Red,
}

public class StageManager : MonoBehaviour
{
    public bool isRandomStage;
    private int randomStagesCleared = 0;
    public GameObject randomCanvas;
    public Canvas menuCanvas; 
    public Canvas settingsCanvas; 
    private bool isPaused = false;
    private bool isPausedFromOutside = false;
    public UIBehaviour scoreTextBehaviour;
    private TextMeshProUGUI scoreTextComponent;
    public UIBehaviour multiplierTextBehaviour;
    public UIBehaviour highScoreTextBehaviour;
    private TextMeshProUGUI multiplierTextComponent;
    public TextWobble multiplierTextWobble;
    public TextWobble multiplierValueTextWobble;
    public TextWobble scoreTextWobble;
    public TextWobble scoreValueTextWobble;


    public WallType solidWall = WallType.Red;
    private readonly List<Wall> subscribedWalls = new();

    public Wave[] waves; 

    public int score;
    public int multiplier;
    public int highestMultiplier;
    public int hitCount;
    private int highscore;
    private bool isHighscore = false;
    private float scoreTickTime = 1;

    private int index = 0;
    private float currentTime;

    private CameraShake cameraShake;
    public Settings settings;
    private System.Random random;

    public static StageManager GetInstance() {
        return FindFirstObjectByType<StageManager>();
    }

    void Start() {
        PlayerPrefs.DeleteAll();
        Cursor.visible = false;
        scoreTextComponent = scoreTextBehaviour.GetComponent<TextMeshProUGUI>();
        multiplierTextComponent = multiplierTextBehaviour.GetComponent<TextMeshProUGUI>();
        highscore = isRandomStage ? PlayerPrefs.GetInt("hs-random") : PlayerPrefs.GetInt("hs");
        highScoreTextBehaviour.GetComponent<TextMeshProUGUI>().text = highscore.ToString();
        cameraShake = CameraShake.GetInstance();
        settings = Settings.GetInstance();
        random = new();
        InstatiateWave();    
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && isPausedFromOutside == false)
        {
            (isPaused ? (Action<bool>)ResumeGame : PauseGame)(true); 
        }

        scoreTickTime -= Time.deltaTime;
        if (scoreTickTime <= 0) {
            score += 100 * multiplier;
            scoreTickTime = 1;
            scoreTextComponent.text = score.ToString();

            if (score > highscore && !isHighscore) {
                isHighscore = true;
                scoreTextWobble.StartWobble();
                scoreValueTextWobble.StartWobble();
            }
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

        index++;
        InstatiateWave();
    }

    private void InstatiateWave() {
        if (isRandomStage) {
            InstatiateWaveRandom();
            return;
        }

        Wave currentWave = waves[index];

        if (currentWave.isTutorial && !settings.ShouldShowTutorial) {
            currentTime = currentWave.nextWaveDelay;
            return;
        }

        foreach (SpawnableObject waveEnemy in currentWave.enemies) {
            Instantiate(
                waveEnemy.enemy,
                waveEnemy.position,
                transform.rotation
            );
        }

        currentTime = currentWave.nextWaveDelay;
        if (currentWave.shouldAwardMultiplier) {
            multiplier++;
            highestMultiplier = highestMultiplier < multiplier ? multiplier : highestMultiplier;
            multiplierTextComponent.text = multiplier.ToString();
        }
    }

    private void InstatiateWaveRandom() {
        Wave currentWave = waves[random.Next(0, waves.Length)];
        currentTime = (float)random.NextDouble() + (2f - (randomStagesCleared < 20 ? randomStagesCleared * .05f : 1));
        foreach (SpawnableObject waveEnemy in currentWave.enemies) {
            Instantiate(
                waveEnemy.enemy,
                waveEnemy.position,
                transform.rotation
            );
        }
        multiplier++;
        highestMultiplier = highestMultiplier < multiplier ? multiplier : highestMultiplier;
        multiplierTextComponent.text = multiplier.ToString();
        randomStagesCleared += 1;
    }

    public void AddWall(Wall wall) {
        subscribedWalls.Add(wall);
    }

    public void RemoveWall(Wall wall) {
        subscribedWalls.Remove(wall);
    }

    public void UpdateUI() {
        scoreTextComponent.text = score.ToString();
        multiplierTextComponent.text = multiplier.ToString();
    }

    public void OnSettingsClicked() {
        menuCanvas.gameObject.SetActive(!menuCanvas.isActiveAndEnabled);
        settingsCanvas.gameObject.SetActive(!settingsCanvas.isActiveAndEnabled);
    }

    public void PauseGame(bool showMenu = false)
    {
        if (showMenu) {
            Cursor.visible = true;
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
            Cursor.visible = false;
            menuCanvas.gameObject.SetActive(false);
            settingsCanvas.gameObject.SetActive(false);
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

    public void ExitToMenu() {
        ResumeGame();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex - 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ShakeCamera() {
        cameraShake.Shake();
    }

    public void OnPlayerHit() {
        if (isRandomStage) {
            Invoke(nameof(InstantiateRandomCanvas), .5f);
            return;
        }

        multiplier = 1;
        hitCount++;
        UpdateUI();
        multiplierTextWobble.TriggerGlitchEffect(.5f);
        multiplierValueTextWobble.TriggerGlitchEffect(.5f);
    }

    private void InstantiateRandomCanvas() {
        Instantiate(randomCanvas);
    }

    void OnDestroy()
    {
        cameraShake.Destroy();
    }
}