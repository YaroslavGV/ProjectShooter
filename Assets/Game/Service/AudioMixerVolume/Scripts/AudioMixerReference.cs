using System;
using UnityEngine.Audio;

[Serializable]
public struct AudioMixerReference
{
    public AudioMixer mixer;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;
}
