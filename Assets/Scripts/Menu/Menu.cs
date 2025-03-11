using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle toggleScreenShake;
    public Toggle toggleShowTutorial;
    public Toggle toggleFullscreen;
    public Canvas mainCanvas;
    public Canvas settingsCanvas;
    public Canvas creditsCanvas;
    public Canvas playgameCanvas;
    private Settings settings;

    void Start() {
        settings = Settings.GetInstance();
        volumeSlider.value = settings.Volume;
        toggleScreenShake.isOn = settings.ShouldScreenShake;
        toggleShowTutorial.isOn = settings.ShouldShowTutorial;
        toggleFullscreen.isOn = settings.FullScreen;
    }

    public void OnClickGamemode(int value) {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + value;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void OnClickPlayGame() {
        mainCanvas.gameObject.SetActive(false);
        playgameCanvas.gameObject.SetActive(true);
    }

    public void OnClickSetting() {
        mainCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void OnClickCredits() {
        mainCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
    }

    public void OnClickBack() {
        mainCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
        playgameCanvas.gameObject.SetActive(false);
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

    public void OnFullScreenChange() {
        settings.FullScreen = toggleFullscreen.isOn;
    }
}
