using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PatternType {
    Circle
}

[CreateAssetMenu(menuName = "ProjectileActions/PatternAction")]
public class ProjectilePatternAction : ProjectileAction
{
    public PatternType patternType;
    public int count;
    public float offset;

    public List<Projectile> projectiles = new();
    public GameObject projectilePrefab;

    public override void Execute()
    {
        
    }

    public override void Init() {
        switch (patternType) {
            case PatternType.Circle: InitCircle(); break;
        }
        projectile.NextAction();
    }

    private void InitCircle() {
        float angleStep = 360f / count;
        
        for (int i = 0; i < count; i++)
        {
            float angle = (i * angleStep + offset ) * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);

            var newAction = CreateInstance<ProjectileRelativePositionAction>();
            newAction.direction = new Vector3(x, y, 0);
            newAction.speed = 5;

            if (i == 0) {
                projectiles.Add(projectile);
                projectile.actionList[0] = newAction;
                continue;
            }

            var newProjectile = Instantiate(projectilePrefab, projectile.transform.position, Quaternion.identity);
            Projectile newProjectileScript = newProjectile.GetComponent<Projectile>();
            newProjectileScript.actionList[0] = newAction;
        }
    }
}