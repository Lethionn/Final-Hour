using strange.extensions.mediation.impl;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Script.Runtime.Context.Menu.Scripts.View.SettingsPanel
{
  public class SettingsPanelView : EventView
  {
    public Slider tiltSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    private PlayerInputActions _playerInputActions;

    private InputAction _optionsAction;

    protected override void Awake()
    {
      _playerInputActions = new PlayerInputActions();
    }
    
    public void SetOptionsAction()
    {
      _optionsAction = _playerInputActions.UI.Options;
      _optionsAction.Enable();
      _optionsAction.performed += (_ => { dispatcher.Dispatch(SettingsEvents.Close); });
    }
    
    public void RemoveOptionsAction()
    {
      _optionsAction.Disable();
      _optionsAction.performed -= (_ => { dispatcher.Dispatch(SettingsEvents.Close); });
    }

    public void ToggleSfx(bool isOn)
    {
      dispatcher.Dispatch(SettingsEvents.ToggleSfx);
    }

    public void ToggleMusic(bool isOn)
    {
      dispatcher.Dispatch(SettingsEvents.ToggleMusic);
    }
    
    public void TiltSensitivity(float value)
    {
      dispatcher.Dispatch(SettingsEvents.TiltSensitivity, tiltSlider.value);
    }

    public void MusicVolume(float value)
    {
      dispatcher.Dispatch(SettingsEvents.MusicVolume, musicSlider.value);
    }

    public void SfxVolume(float value)
    {
      dispatcher.Dispatch(SettingsEvents.SfxVolume, sfxSlider.value);
    }

    public void OnClose()
    {
      dispatcher.Dispatch(SettingsEvents.Close);
    }
  }
}
