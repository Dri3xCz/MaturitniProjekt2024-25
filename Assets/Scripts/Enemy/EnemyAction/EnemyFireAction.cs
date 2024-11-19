using UnityEngine;

[CreateAssetMenu(menuName = "EnemyActions/FireAction")]
public class EnemyFireAction : EnemyAction
{
    public GameObject[] projectiles;

    public override void Execute()
    {
        foreach (GameObject projectile in projectiles) {
            Instantiate(projectile, enemy.transform.position, enemy.transform.rotation);
        }
        enemy.NextAction();
    }

    public override void Init() {}
}