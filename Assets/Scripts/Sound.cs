using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;

    public AudioClip clip;

    public AudioSource source;

    public void Play()
    {
        source.Play();
        source.loop = true;
    }

    public void SetVolume(float volumeModifier)
    {
        source.volume = this.volume * volumeModifier;
    }
}
