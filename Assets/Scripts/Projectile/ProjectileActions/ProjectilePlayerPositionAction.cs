using UnityEngine;

[CreateAssetMenu(menuName = "ProjectileActions/PlayerPositionAction")]
public class ProjectilePlayerPositionAction : ProjectileAction
{
    public Vector3 offset;
    public float deviation;
    public int speed;

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
        System.Random random = new System.Random();

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        direction = playerPos + offset - projectile.transform.position;
        direction.x += ((float)random.NextDouble() * 2 - 1) * deviation;
        direction.y += ((float)random.NextDouble() * 2 - 1) * deviation;

        direction.Normalize();
    }
}