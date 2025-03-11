using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FinalScreen : MonoBehaviour {
    private StageManager sm;
    public UIBehaviour score;
    public UIBehaviour highestMultiplier;
    public UIBehaviour hitCount;

    void Start()
    {
        sm = StageManager.GetInstance();
        Cursor.visible = true;
        sm.PauseGame();
        score.GetComponent<TextMeshProUGUI>().text = sm.score.ToString();
        highestMultiplier.GetComponent<TextMeshProUGUI>().text = sm.highestMultiplier.ToString();
        hitCount.GetComponent<TextMeshProUGUI>().text = sm.hitCount.ToString();

        if (PlayerPrefs.GetInt("hs") < sm.score) {
          PlayerPrefs.SetInt("hs", sm.score);
        }
    }

    void Update()
    {
        if (Input.anyKey) {
          sm.ResumeGame();
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}