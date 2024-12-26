using UnityEngine;

[CreateAssetMenu(menuName = "ProjectileActions/RelativePositionAction")]
public class ProjectileRelativePositionAction : ProjectileAction
{
    public int speed;
    public float offset;

    public override void Execute()
    {
        projectile.transform.position = Vector3.MoveTowards(
            projectile.transform.position,
            projectile.transform.position + direction,
            speed * Time.deltaTime
        );        

        Vector3 projectilePosition = projectile.transform.position;

        if (projectilePosition.y < -10 
            || projectilePosition.y > 10 
            || projectilePosition.x > 10 
            || projectilePosition.x < -10
        ) {
            projectile.NextAction();
        }
    }

    public override void Init() {}
}