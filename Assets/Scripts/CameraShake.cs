using UnityEngine;

public class CameraShake {
#nullable enable
  private static CameraShake? instance;
#nullable disable
  private Camera camera;
  public static CameraShake getInstance() => 
    instance ??= Init();

  private CameraShake(Camera camera) {
    this.camera = camera;
  }

  private static CameraShake Init() {
    Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    return new CameraShake(camera);
  }

  public void Shake() {
    camera.GetComponent<Animation>().Play(); 
  }  
}