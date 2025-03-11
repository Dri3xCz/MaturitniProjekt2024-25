using UnityEngine;

[CreateAssetMenu(menuName = "ProjectileActions/RandomPositionAction")]
public class ProjectileRandomPositionAction : ProjectileAction
{
    public int speed;
    public Vector2 shift;

    public override void Execute()
    {
        projectile.transform.position += direction * speed * Time.deltaTime;
        
        Vector3 projectilePosition = projectile.transform.position;
        if (projectilePosition.y < -10 
            || projectilePosition.y > 10 
            || projectilePosition.x > 10 
            || projectilePosition.x < -10
        ) {
            projectile.NextAction();
        }
    }

    public override void Init() {
        System.Random random = new();

        float x = (float)random.NextDouble() * 2 - 1 + shift.x;
        x = Mathf.Abs(x) < .25f ? (x < 0 ? -.25f : .25f) : x;
        float y = (float)random.NextDouble() * 2 - 1 + shift.y;
        y = Mathf.Abs(y) < .25f ? (y < 0 ? -.25f : .25f) : y;
        direction = new Vector3(x, y, 0);
    }
}