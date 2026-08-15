using System.Collections.Generic;
using Assets.Script.Runtime.Context.Game.Scripts.Enum;
using Assets.Script.Runtime.Context.Menu.Scripts.Enum;
using strange.extensions.dispatcher.impl;
using UnityEngine;

namespace Assets.Script.Runtime.Context.Game.Scripts.Model
{
  public class AudioModel : IAudioModel
  {
    public AudioSource musicSource { get; set; }
    public AudioSource sfxSource { get; set; }
    public AudioSource timeSpeedSource { get; set; }
    public AudioSource deathSoundSource { get; set; }
    
    public AudioSource uiSource { get; set; }

    private bool _scaledMusicVolume;
    private bool _scaledSfxVolume;
    

    public void SetMusicVolume(float volume)
    {
      if (_scaledMusicVolume)
      {
        float currentScale = musicSource.volume / PlayerPrefs.GetFloat(SettingKeys.MusicVolume);
        PlayerPrefs.SetFloat(SettingKeys.MusicVolume, volume);
        musicSource.volume = currentScale * volume;
      }
      else
      {
        PlayerPrefs.SetFloat(SettingKeys.MusicVolume, volume);
        musicSource.volume = volume;
      }
    }
    
    public void SetSfxVolume(float volume)
    {
      if (_scaledSfxVolume)
      {
        float currentScale = sfxSource.volume / PlayerPrefs.GetFloat(SettingKeys.SfxVolume);
        PlayerPrefs.SetFloat(SettingKeys.SfxVolume, volume);
        sfxSource.volume = currentScale * volume;
      }
      else
      {
        PlayerPrefs.SetFloat(SettingKeys.SfxVolume, volume);
        sfxSource.volume = volume;
      }

      uiSource.volume = volume;
      deathSoundSource.volume = volume;
      timeSpeedSource.volume = volume;
    }
    
    public void SetPitchVolumeRelative(float volume, float pitch)
    {
      musicSource.volume = GetScaledValue(volume, PlayerPrefs.GetFloat(SettingKeys.MusicVolume)*0.2f, PlayerPrefs.GetFloat(SettingKeys.MusicVolume));
      musicSource.pitch = GetScaledValue(pitch, 0.5f, 1f);
      
      sfxSource.volume = GetScaledValue(volume, 0, PlayerPrefs.GetFloat(SettingKeys.SfxVolume));
      sfxSource.pitch = GetScaledValue(pitch, 0.95f, 1f);

      _scaledMusicVolume = true;
      _scaledSfxVolume = true;
    }
    
    public void SetPitchVolume(float volume, float pitch)
    {
      musicSource.volume = volume;
      musicSource.pitch = pitch;
      
      sfxSource.volume = volume;
      sfxSource.pitch = pitch;
      
      _scaledMusicVolume = true;
      _scaledSfxVolume = true;
    }
    
    public void ResetPitchVolume()
    { 
      musicSource.mute = PlayerPrefs.GetInt(SettingKeys.MuteMusic) > 0;
      timeSpeedSource.mute = PlayerPrefs.GetInt(SettingKeys.MuteSfx) > 0;
      deathSoundSource.mute = PlayerPrefs.GetInt(SettingKeys.MuteSfx) > 0;
      sfxSource.mute = PlayerPrefs.GetInt(SettingKeys.MuteSfx) > 0;
      uiSource.mute = PlayerPrefs.GetInt(SettingKeys.MuteSfx) > 0;
      
      if (!PlayerPrefs.HasKey(SettingKeys.MusicVolume))
      {
        PlayerPrefs.SetFloat(SettingKeys.MusicVolume, GameMechanicSettings.DefaultMusicVolume);
      }
      
      if (!PlayerPrefs.HasKey(SettingKeys.SfxVolume))
      {
        PlayerPrefs.SetFloat(SettingKeys.SfxVolume, GameMechanicSettings.DefaultSfxVolume);
      }
      
      if (!PlayerPrefs.HasKey(SettingKeys.MuteMusic))
      {
        PlayerPrefs.SetInt(SettingKeys.MuteMusic, 0);
      }
      
      if (!PlayerPrefs.HasKey(SettingKeys.MuteSfx))
      {
        PlayerPrefs.SetInt(SettingKeys.MuteSfx, 0);
      }
      
      musicSource.volume = PlayerPrefs.GetFloat(SettingKeys.MusicVolume);
      musicSource.pitch = 1;
      
      sfxSource.volume = PlayerPrefs.GetFloat(SettingKeys.SfxVolume);
      sfxSource.pitch = 1;
      
      uiSource.volume = PlayerPrefs.GetFloat(SettingKeys.SfxVolume);
      deathSoundSource.volume = PlayerPrefs.GetFloat(SettingKeys.SfxVolume);
      timeSpeedSource.volume = PlayerPrefs.GetFloat(SettingKeys.SfxVolume);
      
      _scaledMusicVolume = false;
      _scaledSfxVolume = false;
    }

    public void ToggleMusic()
    {
      if (PlayerPrefs.GetInt(SettingKeys.MuteMusic) < 1)
      {
        musicSource.mute = true;
        PlayerPrefs.SetInt(SettingKeys.MuteMusic, 1);
      }
      else
      {
        musicSource.mute = false;
        PlayerPrefs.SetInt(SettingKeys.MuteMusic, 0);
      }
    }

    public void ToggleSfx()
    {
      if (PlayerPrefs.GetInt(SettingKeys.MuteSfx) < 1)
      {
        timeSpeedSource.mute = true;
        deathSoundSource.mute = true;
        sfxSource.mute = true;
        uiSource.mute = true;
        PlayerPrefs.SetInt(SettingKeys.MuteSfx, 1);
      }
      else
      {
        timeSpeedSource.mute = false;
        deathSoundSource.mute = false;
        sfxSource.mute = false;
        uiSource.mute = false;
        PlayerPrefs.SetInt(SettingKeys.MuteSfx, 0);
      }
    }

    private float GetScaledValue(float value, float min, float max)
    {
      float range = max - min;

      return min + (range * value);
    }

    public void Pause()
    {
      musicSource.Pause();
      
      sfxSource.Pause();
      
      timeSpeedSource.Pause();
      
      deathSoundSource.Pause();
    }
    
    public void Resume()
    {
      musicSource.UnPause();
      
      sfxSource.UnPause();
      
      timeSpeedSource.UnPause();
      
      deathSoundSource.UnPause();
    }
  }
}