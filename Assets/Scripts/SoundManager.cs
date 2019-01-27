using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    public Sound[] Sounds;
    public Sound[] Background;

    public UnityEvent PlayerEnteredEvent;

    private void Start()
    {
        foreach (var sound in Sounds)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.playOnAwake = false;
            source.loop = false;
            sound.source = source;
        }
        foreach (var sound in Background)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.playOnAwake = false;
            source.loop = true;
            sound.source = source;
            sound.SetVolume(1.0f);
            sound.Play();
        }
    }

    public void AdjustVolume(float volume)
    {
        foreach (var sound in Sounds)
        {
            sound.SetVolume(volume);
        }
        foreach (var sound in Background)
        {
            sound.SetVolume(volume);
        }
    }

    public void PlayerEntry()
    {
        StartCoroutine(PlayEntry());
    }

    public void PlayerLeaving()
    {
        StartCoroutine(PlayLeaving());
    }

    public Sound PlaySoundByName(string name)
    {
        var snd = new Sound();
        foreach (var sound in Sounds)
        {
            if(sound.name == name)
            {
                sound.Play();
                sound.source.loop = false;
                snd = sound;
            }
        }

        return snd;
    }

    IEnumerator PlayEntry()
    {
        var sound = PlaySoundByName("door_enter");
        yield return new WaitForSeconds(sound.source.clip.length);
        sound = PlaySoundByName("steps");
        yield return new WaitForSeconds(sound.source.clip.length);
        sound = PlaySoundByName("bell");
        PlayerEnteredEvent.Invoke();
    }

    IEnumerator PlayLeaving()
    {
        var sound = PlaySoundByName("steps");
        yield return new WaitForSeconds(sound.source.clip.length);
        sound = PlaySoundByName("door_exit");
        yield return new WaitForSeconds(sound.source.clip.length);
    }
}
