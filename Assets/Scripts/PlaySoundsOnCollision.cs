using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsOnCollision : MonoBehaviour
{
    AudioSource audio;
    public float volumeOnStay = 0.6f;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audio.loop)
            return;

        float magnitude = collision.relativeVelocity.magnitude;

        audio.volume = 1 - (1 / magnitude);
        audio.Play();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (PauseGame.isGamePaused && audio.loop)
        {
            audio.volume = 0;
            return;
        }

        if (audio.loop)
            audio.volume = volumeOnStay;
    }

    private void OnCollisionExit(Collision collision)
    {
        if(audio.loop)
            audio.volume = 0;
    }
}