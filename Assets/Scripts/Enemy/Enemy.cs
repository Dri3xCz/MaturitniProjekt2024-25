using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyAction[] actionList;

    private int index = 0;
    private EnemyAction currentAction;

    void Start() {
        SetupCurrentAction();
    }

    void Update() {
        currentAction.Execute();
    } 

    public void NextAction() {
        if (index >= actionList.Length - 1) {
            Destroy(gameObject);
            return;
        };
        index++;
        Destroy(currentAction);
        SetupCurrentAction();
    }

    public void SetupCurrentAction() {
        currentAction = Instantiate(actionList[index]);
        currentAction.enemy = this;
        currentAction.nextAction = NextAction;
        currentAction.Init();
    }
}
