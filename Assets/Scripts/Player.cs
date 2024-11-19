using UnityEngine;

public class Player : MonoBehaviour
{
    public int Speed;

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
    }
}
