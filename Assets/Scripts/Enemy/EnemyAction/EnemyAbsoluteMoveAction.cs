using UnityEngine;

[CreateAssetMenu(menuName = "EnemyActions/AbsoluteMoveAction")]
public class EnemyAbsoluteMoveAction : EnemyAction
{
    public int speed;
    public Vector3 destination;

    public override void Execute()
    {
        enemy.transform.position = Vector3.MoveTowards(
            enemy.transform.position,
            destination,
            speed * Time.deltaTime
        );        

        if (destination == enemy.transform.position) {
            nextAction();
        }
    }

    public override void Init() {}
}