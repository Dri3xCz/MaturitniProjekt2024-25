using UnityEngine;

public delegate void NextAction();

public abstract class EnemyAction : ScriptableObject
{
    public Enemy enemy;
    public abstract void Execute();
    public abstract void Init();
    public NextAction nextAction;
}