using UnityEngine;

public class CameraShake {
#nullable enable
  private static CameraShake? instance;
#nullable disable
  private Camera camera;
  public static CameraShake GetInstance() => 
    instance ??= Init();

  private bool shouldScreenShake;
  private Settings settings;

  private void UpdateSettings() {
    shouldScreenShake = settings.ShouldScreenShake;
  }

  private CameraShake(Camera camera, Settings settings) {
    this.camera = camera;
    this.settings = settings;
  }

  private static CameraShake Init() {
    Settings settings = Settings.GetInstance();
    Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    CameraShake cameraShake = new CameraShake(camera, settings);
    settings.SubscribeToChanges(cameraShake.UpdateSettings);
    cameraShake.UpdateSettings();

    return cameraShake;
  }

  public void Shake() {
    if (!shouldScreenShake) return;

    camera.GetComponent<Animation>().Play(); 
  }  

  public void Destroy() {
    instance = null;
  }
}