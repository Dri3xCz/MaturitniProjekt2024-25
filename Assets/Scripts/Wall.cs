using UnityEngine;

public class Wall : MonoBehaviour
{
    public WallType type;
    public bool state;
    public Vector3 direction;
    public bool isVertical;
    private StageManager sm;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    void Start() {
        if (isVertical) {
            GetComponent<Transform>().Rotate(new Vector3(0, 0, 90));
        }

        sm = StageManager.GetInstance();
        sm.AddWall(this);
        state = sm.solidWall == type;
        
        sr = GetComponent<SpriteRenderer>();
        sr.color = state ? Color.red : Color.white;

        bc = GetComponent<BoxCollider2D>();
        bc.enabled = state;
    }

    void Update()
    {
        transform.position = new Vector3(
            transform.position.x + direction.x * Time.deltaTime,
            transform.position.y + direction.y * Time.deltaTime,
            0
        );    

        if (transform.position.y < -10 || Mathf.Abs(transform.position.x) > 10) {
            DestroyProperly();              
        }
    }

    public void ChangeState() {
        state = !state;
        sr.color = state ? Color.red : Color.white;
        bc.enabled = state;
    }

    public void DestroyProperly() {
        sm.RemoveWall(this);
        Destroy(this.gameObject);
    }
}
