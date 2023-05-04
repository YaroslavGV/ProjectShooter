using System;
using UnityEngine;
using Zenject;
using Memento;

public class AudioMixerVolumeInstaller : MonoInstaller
{
    [SerializeField] private string _saveKey = "AudioMixerVolume";
    [Space]
    [SerializeField] private AudioMixerReference _audio;
    [SerializeField] private AudioVolume _default = new AudioVolume(1, 1);
    [Space]
    [SerializeField] private bool _log = true;
    private MementoAudioMixerVolume _audioVolume;
    private JsonPlayerPrefsHandler _jsonHandler;

    public override void InstallBindings ()
    {
        if (string.IsNullOrEmpty(_saveKey))
            throw new Exception("SaveKey is null or empty");
        if (_audio.mixer == null || _audio.musicGroup == null || _audio.sfxGroup == null)
            throw new Exception("Audio references are not filled");

        _audioVolume = new MementoAudioMixerVolume(_audio);
        Container.Bind<AudioMixerVolume>().FromInstance(_audioVolume).AsSingle();
        _jsonHandler = new JsonPlayerPrefsHandler(_saveKey, _audioVolume, GetDefaultJson, false);
    }

    public override void Start ()
    {
        base.Start();
        _jsonHandler.Load();

        if (_log)
        {
            string text = ObjectLog.GetText(_audioVolume, _saveKey);
            Debug.Log(text);
        }
    }

    private string GetDefaultJson ()
    {
        MementoAudioMixerVolume defaultAudioVolume = new MementoAudioMixerVolume(_audio);
        defaultAudioVolume.Volume = _default;
        return defaultAudioVolume.GetJson();
    }

    [ContextMenu("ClearData")]
    private void ClearData ()
    {
        PlayerPrefs.DeleteKey(_saveKey);
        PlayerPrefs.Save();
    }
}