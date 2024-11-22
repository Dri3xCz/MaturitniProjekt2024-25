using UnityEngine;

public class Music : MonoBehaviour
{
    public string soundName;

    private AudioSource audioSource;
    private MusicManager mm;
    private float volumeConstant;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        mm = MusicManager.getInstance();
        volumeConstant = mm.volumeConstants[soundName];
        
        ChangeVolume();
    }

    public void ChangeVolume() {
        audioSource.volume = mm.GameVolume * volumeConstant;
    }
}
