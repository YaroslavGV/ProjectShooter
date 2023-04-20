using System;
using UnityEngine;
using Memento;

public class MementoAudioMixerVolume : IJsonContent
{
    private readonly AudioMixerVolume _audio;
    private readonly AudioVolume _default;

    public MementoAudioMixerVolume (AudioMixerVolume audio, AudioVolume defaultVolume)
    {
        _audio = audio;
        _default = defaultVolume;

        _audio.OnChange += OnChange;
    }

    ~MementoAudioMixerVolume () => _audio.OnChange -= OnChange;

    public Action ContentUpdated { get; set; }

    public string GetJson () => JsonUtility.ToJson(_audio.Volume);

    public void SetJson (string json) => _audio.Volume = JsonUtility.FromJson<AudioVolume>(json);

    public void SetDefault () => _audio.Volume = _default;

    private void OnChange () => ContentUpdated?.Invoke();
}
