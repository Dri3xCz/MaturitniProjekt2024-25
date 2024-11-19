using UnityEngine;

public class MusicMain : MonoBehaviour
{
    private AudioSource audioSource;
    private GameManager gm;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        gm = GameManager.getInstance();
        audioSource.volume = gm.GameVolume/2;
    }
}
