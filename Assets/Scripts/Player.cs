using System.Security;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Speed;
    public Camera mainCamera;
    public ParticleSystem onDeath;
    public bool invincible = false;
    private AudioSource audioSource;
    private new Animation animation;
    private StageManager sm;

    void Start() {
        sm = StageManager.GetInstance();
        audioSource = GetComponent<AudioSource>();
        animation = GetComponent<Animation>();
    }

    void Update()
    {
        HandleMove();        
    }

    void HandleMove() {
    int speed = Input.GetKey(KeyCode.LeftShift) 
        ? Speed / 2 
        : Speed;

    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    float horizontalMove = horizontal * Time.deltaTime * speed;
    float verticalMove = vertical * Time.deltaTime * speed;

    Vector3 position = transform.position;
    transform.position = new Vector3(
        position.x + horizontalMove,
        position.y + verticalMove
    );

    float stretchFactor = 1.3f; 
    float shrinkFactor = 0.6f;  
    float lerpSpeed = 10f; 

    Vector3 targetScale = new Vector3(.5f, .5f, 0);

    if (horizontal != 0 || vertical != 0) {
        float stretchX = Mathf.Abs(horizontal) > 0 ? stretchFactor : 1f;
        float stretchY = Mathf.Abs(vertical) > 0 ? stretchFactor : 1f;
        
        targetScale = new Vector3(shrinkFactor / stretchY, shrinkFactor / stretchX, 1f);
        float targetAngle = Mathf.Atan2(-horizontal, vertical) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), Time.deltaTime * 50f);
    }

    transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * lerpSpeed);

    // Clamping position to camera bounds
    Vector3 scale = transform.localScale;
    float cameraLeft = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect * .9f + scale.x / 2;
    float cameraRight = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect * .4f - scale.x / 2;
    float cameraTop = mainCamera.transform.position.y + mainCamera.orthographicSize * .96f - scale.y / 2;
    float cameraBottom = mainCamera.transform.position.y - mainCamera.orthographicSize * .96f + scale.y / 2;
    Vector3 playerPosition = transform.position;

    playerPosition.x = Mathf.Clamp(playerPosition.x, cameraLeft, cameraRight);
    playerPosition.y = Mathf.Clamp(playerPosition.y, cameraBottom, cameraTop);

    transform.position = playerPosition;
}


    private void OnTriggerEnter2D(Collider2D other) {
        if (invincible) return;
        
        sm.OnPlayerHit();
        HandleHitVisuals();
        if (other.tag == "Wall") {
            other.gameObject.GetComponent<Wall>().DestroyProperly();
            return;
        }

        Destroy(other.gameObject);
    }

    private void HandleHitVisuals() {
        onDeath.Play();
        audioSource.Play();
        animation.Play();

        sm.ShakeCamera();
    }
}
