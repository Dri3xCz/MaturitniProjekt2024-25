using UnityEngine;

public abstract class ProjectileAction : ScriptableObject
{
    public Projectile projectile;
    // Shit code, incase you need to modify direction from outside
    public Vector3 direction;
    public abstract void Execute();
    public abstract void Init();
}