using Assets.Script.Runtime.Context.Game.Scripts.Model;
using Assets.Script.Runtime.Context.Menu.Scripts.Enum;
using Assets.Script.Runtime.Context.Menu.Scripts.Model;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Script.Runtime.Context.Menu.Scripts.View.SettingsPanel
{
  public enum SettingsEvents
  {
    ToggleSfx,
    ToggleMusic,
    TiltSensitivity,
    MusicVolume,
    SfxVolume,
    Close
  }

  public class SettingsPanelMediator : EventMediator
  {
    [Inject]
    public SettingsPanelView view { get; set; }
    
    [Inject]
    public IAudioModel audioModel { get; set; }
    
    [Inject]
    public IUIModel uiModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(SettingsEvents.ToggleSfx, OnToggleSFX);
      view.dispatcher.AddListener(SettingsEvents.ToggleMusic, OnToggleMusic);
      view.dispatcher.AddListener(SettingsEvents.TiltSensitivity, OnTiltSensitivity);
      view.dispatcher.AddListener(SettingsEvents.MusicVolume, OnMusicVolume);
      view.dispatcher.AddListener(SettingsEvents.SfxVolume, OnSfxVolume);
      view.dispatcher.AddListener(SettingsEvents.Close, OnClose);
    }

    public override void OnInitialize()
    {
      view.SetOptionsAction();
      
      view.musicToggle.onValueChanged.RemoveAllListeners();
      view.sfxToggle.onValueChanged.RemoveAllListeners();
      view.musicSlider.onValueChanged.RemoveAllListeners();
      view.sfxSlider.onValueChanged.RemoveAllListeners();
      
      view.musicToggle.isOn = (PlayerPrefs.GetInt(SettingKeys.MuteMusic) < 1);
      view.musicSlider.interactable = (PlayerPrefs.GetInt(SettingKeys.MuteMusic) < 1);
      view.musicSlider.value = PlayerPrefs.GetFloat(SettingKeys.MusicVolume);
      
      view.sfxToggle.isOn = (PlayerPrefs.GetInt(SettingKeys.MuteSfx) < 1);
      view.sfxSlider.interactable = (PlayerPrefs.GetInt(SettingKeys.MuteSfx) < 1);
      view.sfxSlider.value = PlayerPrefs.GetFloat(SettingKeys.SfxVolume);

      view.musicToggle.onValueChanged.AddListener(view.ToggleMusic);
      view.sfxToggle.onValueChanged.AddListener(view.ToggleSfx);
      view.musicSlider.onValueChanged.AddListener(view.MusicVolume);
      view.sfxSlider.onValueChanged.AddListener(view.SfxVolume);
      
#if !UNITY_STANDALONE && !UNITY_WEBGL
      view.tiltSlider.onValueChanged.RemoveAllListeners();
      
      view.tiltSlider.value = view.tiltSlider.maxValue - PlayerPrefs.GetFloat(SettingKeys.TiltSensitivity);
      
      view.tiltSlider.onValueChanged.AddListener(view.TiltSensitivity);
#endif
    }

    private void OnClose()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
      uiModel.ClosePanel(PanelKeys.SettingsPanel);
#else
      uiModel.ClosePanel(PanelKeys.SettingsPanelMobile);
#endif
    }

    private void OnToggleSFX()
    {
      audioModel.ToggleSfx();
      OnInitialize();
    }

    private void OnToggleMusic()
    {
      audioModel.ToggleMusic();
      OnInitialize();
    }
    
    private void OnMusicVolume(IEvent payload)
    {
      float volume = (float)payload.data;
      audioModel.SetMusicVolume(volume);
    }
    
    private void OnTiltSensitivity(IEvent payload)
    {
      float value = (float)payload.data;
      PlayerPrefs.SetFloat(SettingKeys.TiltSensitivity, view.tiltSlider.maxValue - value);
    }

    private void OnSfxVolume(IEvent payload)
    {
      float volume = (float)payload.data;
      audioModel.SetSfxVolume(volume);
    }

    public override void OnRemove()
    {
      view.RemoveOptionsAction();
      
      view.dispatcher.RemoveListener(SettingsEvents.ToggleSfx, OnToggleSFX);
      view.dispatcher.RemoveListener(SettingsEvents.ToggleMusic, OnToggleMusic);
      view.dispatcher.RemoveListener(SettingsEvents.TiltSensitivity, OnTiltSensitivity);
      view.dispatcher.RemoveListener(SettingsEvents.MusicVolume, OnMusicVolume);
      view.dispatcher.RemoveListener(SettingsEvents.SfxVolume, OnSfxVolume);
      view.dispatcher.RemoveListener(SettingsEvents.Close, OnClose);
    }
  }
}
