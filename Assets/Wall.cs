using UnityEngine;

public class Wall : MonoBehaviour
{
    public WallType type;
    public bool state;
    private StageManager sm;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    void Start() {
        sm = StageManager.getInstance();
        sm.AddWall(this);
        state = sm.solidWall == type;
        
        sr = GetComponent<SpriteRenderer>();
        sr.color = state ? Color.red : Color.white;

        bc = GetComponent<BoxCollider2D>();
        bc.enabled = state;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 10 * Time.deltaTime, 0);    

        if (transform.position.y < -10) {
            DestoryProperly();              
        }
    }

    public void ChangeState() {
        state = !state;
        sr.color = state ? Color.red : Color.white;
        bc.enabled = state;
    }

    public void DestoryProperly() {
        sm.RemoveWall(this);
        Destroy(this.gameObject);
    }
}
