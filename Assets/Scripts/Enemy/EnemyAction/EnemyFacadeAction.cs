using UnityEngine;

[CreateAssetMenu(menuName = "EnemyActions/FacadeAction")]
public class EnemyFacadeAction : EnemyAction
{
    public EnemyAction[] actionList;

    private EnemyAction currentAction;
    private int index = 0;

    public override void Execute()
    {
        currentAction.Execute();
    }


    public override void Init() {
        SetupCurrentAction();
    }

    private void NextAction() {
        if (index >= actionList.Length - 1) {
            nextAction();
            return;
        };
        index++;
        Destroy(currentAction);
        SetupCurrentAction();
    }

    public void SetupCurrentAction() {
        currentAction = Instantiate(actionList[index]);
        currentAction.enemy = enemy;
        currentAction.nextAction = NextAction;
        currentAction.Init();
    }
}