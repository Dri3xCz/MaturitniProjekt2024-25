using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public UIBehaviour playGame;
    public UIBehaviour settings;
    public Slider volumeSlider;
    public UIBehaviour back;
    public UIBehaviour volumeText;
    public UIBehaviour quit;

    public SoundPlayer music;

    void Start() {
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<SoundPlayer>();
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
        playGame.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
        volumeSlider.gameObject.SetActive(true);
        volumeText.gameObject.SetActive(true);
        quit.gameObject.SetActive(false);
    }

    public void OnClickBack() {
        playGame.gameObject.SetActive(true);
        settings.gameObject.SetActive(true);
        back.gameObject.SetActive(false);
        volumeSlider.gameObject.SetActive(false);
        volumeText.gameObject.SetActive(false);
        quit.gameObject.SetActive(true);
    }

    public void OnVolumeChange() {
        float value = volumeSlider.value;
        MusicManager.getInstance().GameVolume = value;
        music.ChangeVolume();
    }

    public void OnClickQuit() {
        Application.Quit();
    }
}
