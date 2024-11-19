using UnityEngine;

public abstract class ProjectileAction : ScriptableObject
{
    public Projectile projectile;
    public abstract void Execute();
    public abstract void Init();
}