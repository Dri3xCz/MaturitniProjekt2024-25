using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle toggleScreenShake;
    public Toggle toggleShowTutorial;
    public Canvas mainCanvas;
    public Canvas settingsCanvas;
    private Settings settings;

    void Start() {
        settings = Settings.GetInstance();
        volumeSlider.value = settings.Volume;
        toggleScreenShake.isOn = settings.ShouldScreenShake;
        toggleShowTutorial.isOn = settings.ShouldShowTutorial;
    }

    public void OnClickPlayGame() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void OnClickSetting() {
        mainCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void OnClickBack() {
        mainCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
    }

    public void OnScreenShakeChange() {
        settings.ShouldScreenShake = toggleScreenShake.isOn;
    }

    public void OnVolumeChange() {
        float value = volumeSlider.value;
        settings.Volume = value;
    }

    public void OnShowTutorialChange() {
        settings.ShouldShowTutorial = toggleShowTutorial.isOn;
    }

    public void OnClickQuit() {
        Application.Quit();
    }
}
