using UnityEngine;

[CreateAssetMenu(menuName = "EnemyActions/IdleAction")]
public class EnemyIdleAction : EnemyAction
{
    public float length;
    private float localTime;

    public override void Execute()
    {
        localTime -= Time.deltaTime;

        if (localTime <= 0) {
            enemy.NextAction();
        }
    }

    public override void Init()
    {
        localTime = length;
    }
}