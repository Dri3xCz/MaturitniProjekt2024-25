using UnityEngine;

public class Parallax : MonoBehaviour
{
    public RectTransform background1; 
    public RectTransform background2; 
    public float parallaxStrength1 = 10f; 
    public float parallaxStrength2 = 5f;  

    private Vector2 screenCenter;

    void Start()
    {
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        
        Vector2 normalizedMovement = new(
            (mousePos.x - screenCenter.x) / screenCenter.x,
            (mousePos.y - screenCenter.y) / screenCenter.y
        );

        background1.anchoredPosition = normalizedMovement * parallaxStrength1;
        background2.anchoredPosition = normalizedMovement * parallaxStrength2;
    }
}
