using System.Collections.Generic;
using UnityEngine;

public class SpawnableCanvas : MonoBehaviour {
  public List<KeyCode> keyCodesToExit;

  private StageManager sm;

  void Start() {
    sm = StageManager.getInstance();
    sm.PauseGame();
  }

  void Update() {
    if ((Input.anyKeyDown && keyCodesToExit.Count == 0)
        || keyCodesToExit.Exists((k) => Input.GetKeyDown(k))) {
      Destroy(gameObject);
      sm.ResumeGame();
    } 
  }
}