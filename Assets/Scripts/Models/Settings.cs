using System;

public class Settings {
#nullable enable
  public event Action? SettingsChanged;
  private static Settings? instance;
#nullable disable
  public static Settings GetInstance()
    => instance ??= new Settings(
      .125f,
      true
    );

  private bool shouldScreenShake;
  private float volume;

  public bool ShouldScreenShake 
  { 
    get => shouldScreenShake;
    set {
      shouldScreenShake = value;
      OnSettingsChanged();
    }
  }
  public float Volume 
  { 
    get => volume;
    set {
      volume = value;
      OnSettingsChanged();
    }
  }

  public Settings(float volume, bool shouldScreenShake) {
    Volume = volume;
    ShouldScreenShake = shouldScreenShake;
  }

  protected virtual void OnSettingsChanged()
  {
      SettingsChanged?.Invoke();
  }

  public void SubscribeToChanges(Action handler)
  {
      SettingsChanged += handler;
  }

  public void UnsubscribeFromChanges(Action handler)
  {
      SettingsChanged -= handler;
  }
}