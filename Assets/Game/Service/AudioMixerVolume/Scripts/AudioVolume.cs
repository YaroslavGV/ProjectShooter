using System;

[Serializable]
public struct AudioVolume
{
    public float music;
    public float sfx;

    public AudioVolume (float music, float sfx)
    {
        this.music = music;
        this.sfx = sfx;
    }

    public override string ToString () => string.Format("music: {0}\nsfx: {1}", music, sfx);
}
