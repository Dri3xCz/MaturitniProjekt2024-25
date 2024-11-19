using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyAction[] actionList;

    private int index = 0;
    private EnemyAction currentAction;

    void Start() {
        currentAction = Instantiate(actionList[index]);
        currentAction.enemy = this;
        currentAction.Init();
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
        currentAction = Instantiate(actionList[index]);
        currentAction.enemy = this;
        currentAction.Init();
    }
}
