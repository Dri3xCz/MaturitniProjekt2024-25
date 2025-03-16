using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FinalScreen : MonoBehaviour {
    private StageManager sm;
    private Settings settings;
    public bool isRandomStage = false;
    public UIBehaviour score;
    public UIBehaviour highestMultiplier;
    public UIBehaviour hitCount;
    private float currentTime;


    void Start()
    {
        sm = StageManager.GetInstance();
        settings = Settings.GetInstance();
        Cursor.visible = true;
        sm.PauseGame();
        score.GetComponent<TextMeshProUGUI>().text = sm.score.ToString();
        highestMultiplier.GetComponent<TextMeshProUGUI>().text = sm.highestMultiplier.ToString();

        (isRandomStage ? (Action)HandleRandomFinish : HandleStageFinish)();
        settings.ShouldShowTutorial = isRandomStage ? settings.ShouldShowTutorial : false;

        currentTime = Time.unscaledTime;
    }

    void HandleStageFinish() {
        hitCount.GetComponent<TextMeshProUGUI>().text = sm.hitCount.ToString();

        if (PlayerPrefs.GetInt("hs") < sm.score) {
          PlayerPrefs.SetInt("hs", sm.score);
          PlayerPrefs.Save();
        }
    }

    void HandleRandomFinish() {
        Debug.Log(PlayerPrefs.GetInt("hs-random"));
        Debug.Log(sm.score);

        if (PlayerPrefs.GetInt("hs-random") < sm.score) {
          PlayerPrefs.SetInt("hs-random", sm.score);
          PlayerPrefs.Save();
        }
    }

    void Update()
    {
        if (Time.unscaledTime < currentTime + 1) return;

        if (Input.GetKeyDown(KeyCode.R) && isRandomStage) {
          sm.ResumeGame();
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
          return;
        }

        if (Input.anyKeyDown) {
          sm.ResumeGame();
          SceneManager.LoadScene(0);
        }
    }
}