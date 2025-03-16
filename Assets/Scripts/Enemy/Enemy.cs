using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyAction[] actionList;

    private int index = 0;
    private EnemyAction currentAction;
    private Vector3 currentPosition;
    private Vector3 startScale;

    void Start() {
        SetupCurrentAction();
        currentPosition = transform.position;
        startScale = transform.localScale;
    }

    void Update() {
        currentAction.Execute();
        if (currentPosition == transform.position) {
            transform.localScale = Vector3.Lerp(transform.localScale, startScale, 30f * Time.deltaTime);
            return;
        }
        
        var direction = (transform.position - currentPosition).normalized;
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            new Vector3(
                startScale.x - .1f + Mathf.Abs(direction.x) * .2f,
                startScale.y - .1f + Mathf.Abs(direction.y) * .2f,
                1
            ),
            30f * Time.deltaTime
        );
        currentPosition = transform.position;
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
