using System;
using UnityEngine;

public class AudioMixerVolume
{
    public Action OnChange;
    private readonly AudioMixerReference _audio;
    protected AudioVolume volume;

    public AudioMixerVolume (AudioMixerReference audio)
    {
        _audio = audio;
    }

    public override string ToString () => string.Format("Music: {0}\nSFX: {1}", volume.music, volume.sfx);

    public AudioVolume Volume
    {
        get => volume;
        set
        {
            volume = value;
            UpdateAudioMixer();
            OnChange?.Invoke();
        }
    }

    protected void UpdateAudioMixer ()
    {
        SetVolume(_audio.musicGroup.name, volume.music);
        SetVolume(_audio.sfxGroup.name, volume.sfx);
    }

    private void SetVolume (string groupName, float normalValue)
    {
        float value = normalValue == 0 ? -80 : Mathf.Log10(normalValue) * 20f;
        _audio.mixer.SetFloat(groupName, value);
    }
}
