using UnityEngine;

public class Projectile : MonoBehaviour
{   
    public ProjectileAction[] actionList;

    private int index = 0; 
    public ProjectileAction currentAction;

    void Start() { 
        currentAction = Instantiate(actionList[index]);
        currentAction.projectile = this;
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
        currentAction.projectile = this;
        currentAction.Init();
    }
}
