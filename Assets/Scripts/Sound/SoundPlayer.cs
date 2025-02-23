using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public string soundName;
    private AudioSource audioSource;
    private MusicManager mm;
    private float volumeConstant;
    private Settings settings;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        mm = MusicManager.getInstance();
        settings = Settings.GetInstance();
        settings.SubscribeToChanges(ChangeVolume);
        volumeConstant = mm.volumeConstants[soundName];
        ChangeVolume();
    }

    public void ChangeVolume() {
        audioSource.volume = settings.Volume * volumeConstant;
    }

    private void OnDestroy()
    {
        settings.UnsubscribeFromChanges(ChangeVolume);
    }
}
