using UnityEngine;

[CreateAssetMenu(menuName = "ProjectileActions/SetPositionAction")]
public class ProjectileSetPositionAction : ProjectileAction
{
    public int speed;
    public Vector3 destination;

    public override void Execute()
    {
        Vector3 direction = destination - projectile.transform.position;
        projectile.transform.position = Vector3.MoveTowards(
            projectile.transform.position,
            destination,
            speed * Time.deltaTime
        );        

        if (direction == Vector3.zero) {
            projectile.NextAction();
        }
    }

    public override void Init() {}
}