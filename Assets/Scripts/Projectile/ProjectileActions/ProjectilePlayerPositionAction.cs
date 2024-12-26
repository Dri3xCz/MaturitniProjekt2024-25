using UnityEngine;

[CreateAssetMenu(menuName = "ProjectileActions/PlayerPositionAction")]
public class ProjectilePlayerPositionAction : ProjectileAction
{
    public Vector3 offset;
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
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        direction = playerPos + offset - projectile.transform.position;
        direction.Normalize();
    }
}