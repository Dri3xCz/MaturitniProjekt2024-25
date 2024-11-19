using UnityEngine;

public class Player : MonoBehaviour
{
    public int Speed;
    public Camera mainCamera;
    public int hp = 5;

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

        float cameraLeft = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect * .98f;
        float cameraRight = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect * .6f;
        float cameraTop = mainCamera.transform.position.y + mainCamera.orthographicSize * .98f;
        float cameraBottom = mainCamera.transform.position.y - mainCamera.orthographicSize * .98f;
        Vector3 playerPosition = transform.position;

        playerPosition.x = Mathf.Clamp(playerPosition.x, cameraLeft, cameraRight);
        playerPosition.y = Mathf.Clamp(playerPosition.y, cameraBottom, cameraTop);

        transform.position = playerPosition;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Trigger enter");
        hp--;
        Destroy(other.gameObject);
    } 
}
