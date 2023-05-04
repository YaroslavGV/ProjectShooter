using System;
using UnityEngine;
using Memento;

public class MementoAudioMixerVolume : AudioMixerVolume, IJsonContent
{
    public MementoAudioMixerVolume (AudioMixerReference audio) : base(audio) => OnChange += OnContentChange;

    ~MementoAudioMixerVolume () => OnChange -= OnContentChange;

    public Action ContentUpdated { get; set; }

    public string GetJson () => JsonUtility.ToJson(Volume);

    public void SetJson (string json) 
    {
        volume = JsonUtility.FromJson<AudioVolume>(json);
        UpdateAudioMixer();
    }

    private void OnContentChange () => ContentUpdated?.Invoke();
}
