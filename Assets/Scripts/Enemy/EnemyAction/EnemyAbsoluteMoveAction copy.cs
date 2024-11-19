using UnityEngine;

[CreateAssetMenu(menuName = "EnemyActions/RelativeMoveAction")]
public class EnemyRelativeMoveAction : EnemyAction
{
    public int speed;
    public Vector3 direction;
    
    private Vector3 destination;

    public override void Execute()
    {
        enemy.transform.position = Vector3.MoveTowards(
            enemy.transform.position,
            destination,
            speed * Time.deltaTime
        );        

        if (destination == enemy.transform.position) {
            enemy.NextAction();
        }
    }

    public override void Init() {
        destination = enemy.transform.position + direction;
    }
}