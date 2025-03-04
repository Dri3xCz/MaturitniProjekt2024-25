using System;
using UnityEngine;

public class Settings {
#nullable enable
  public event Action? SettingsChanged;
  private static Settings? instance;
#nullable disable
  public static Settings GetInstance()
    => instance ??= new Settings(
      PlayerPrefs.HasKey("Volume") ? PlayerPrefs.GetFloat("Volume") : .125f,
      PlayerPrefs.HasKey("ShouldScreenShake") ? PlayerPrefs.GetInt("ShouldScreenShake") == 1 : true,
      PlayerPrefs.HasKey("ShouldShowTutorial") ? PlayerPrefs.GetInt("ShouldShowTutorial") == 1 : true
    );

  private bool shouldScreenShake;
  private float volume;
  private bool shouldShowTutorial;

  public bool ShouldScreenShake 
  { 
    get => shouldScreenShake;
    set {
      shouldScreenShake = value;
      PlayerPrefs.SetInt("ShouldScreenShake", value ? 1 : 0);
      OnSettingsChanged();
    }
  }
  public float Volume 
  { 
    get => volume;
    set {
      volume = value;
      PlayerPrefs.SetFloat("Volume", value);
      OnSettingsChanged();
    }
  }
  public bool ShouldShowTutorial
  { 
    get => shouldShowTutorial;
    set {
      shouldShowTutorial = value;
      PlayerPrefs.SetInt("ShouldShowTutorial", value ? 1 : 0);
      OnSettingsChanged();
    }
  }

  public Settings(float volume, bool shouldScreenShake, bool shouldShowTutorial) {
    Volume = volume;
    ShouldScreenShake = shouldScreenShake;
    ShouldShowTutorial = shouldShowTutorial;
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