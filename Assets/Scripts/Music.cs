using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource audioSource;
    private GameManager gm;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        gm = GameManager.getInstance();
        audioSource.volume = gm.GameVolume;
    }

    public void ChangeVolume() {
        audioSource.volume = gm.GameVolume;
    }
}
