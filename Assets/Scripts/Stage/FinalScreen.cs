using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FinalScreen : MonoBehaviour {
    private StageManager sm;
    public bool isRandomStage = false;
    public UIBehaviour score;
    public UIBehaviour highestMultiplier;
    public UIBehaviour hitCount;
    private float currentTime;


    void Start()
    {
        sm = StageManager.GetInstance();
        Cursor.visible = true;
        sm.PauseGame();
        score.GetComponent<TextMeshProUGUI>().text = sm.score.ToString();
        highestMultiplier.GetComponent<TextMeshProUGUI>().text = sm.highestMultiplier.ToString();

        (isRandomStage ? (Action)HandleRandomFinish : HandleStageFinish)();

        currentTime = Time.unscaledTime;
    }

    void HandleStageFinish() {
        hitCount.GetComponent<TextMeshProUGUI>().text = sm.hitCount.ToString();

        if (PlayerPrefs.GetInt("hs") < sm.score) {
          PlayerPrefs.SetInt("hs", sm.score);
        }
    }

    void HandleRandomFinish() {
        if (PlayerPrefs.GetInt("hs-random") < sm.score) {
          PlayerPrefs.SetInt("hs-random", sm.score);
        }
    }

    void Update()
    {
        if (Time.unscaledTime < currentTime + 1) return;

        if (Input.anyKey) {
          sm.ResumeGame();
          SceneManager.LoadScene(0);
        }
    }
}