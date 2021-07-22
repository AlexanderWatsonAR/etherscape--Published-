using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for animation events.
public class PlaySound : MonoBehaviour
{
    public AudioClip nextClip;
    public AudioClip[] extras;

    public GameObject otherAudioSource;

    AudioSource audio;

    public void Start()
    {
        audio = GetComponent<AudioSource>();
        if(otherAudioSource != null)
            audio = otherAudioSource.GetComponent<AudioSource>();
    }

    public void Play()
    {
        if(audio != null)
            audio.Play();
    }

    public void PlayNextClip()
    {
        if (audio != null && nextClip != null)
        {
            audio.clip = nextClip;
            Play();
        }
    }

    public void PlayExtra(int index)
    {
        if (audio != null)
        {
            audio.clip = extras[index];
            Play();
        }
    }
    public void Volume(float volume)
    {
        if (audio != null)
            audio.volume = volume;
    }
    public void Pitch(float pitch)
    {
        if (audio != null)
            audio.pitch = pitch;
    }
    public void Loop(int loop)
    {
        if (audio == null)
            return;

        switch (loop)
        {
            case 0:
                audio.loop = false;
                break;
            case 1:
                audio.loop = true;
                break;
        }
    }
    public void Stop()
    {
        if (audio != null)
            audio.Stop();
    }

}
