using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource MusicSource;
    public AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip MouseClick;
    public AudioClip FoxSound;
    public AudioClip CrowSound;
    public AudioClip Pause;
    public AudioClip Jump;
    public AudioClip wingsFlap;
    public AudioClip Lever;
    public AudioClip DoorOpen;
    public AudioClip Buttton;
    public AudioClip pipe;
    public AudioClip trashsound;
    public AudioClip Victory;
    public AudioClip FoxFootSteps;

    private void Start()
    {

    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void playMyAudio(AudioClip clipToPlay) // You can't use "Stop" with "PlayOneShot", you have to use "Play"
    {
        SFXSource.clip = clipToPlay;
        SFXSource.Play();
    }

    public void StopSFX(AudioClip clip)
    {
        if (SFXSource.isPlaying && SFXSource.clip == clip)
        {
            SFXSource.Stop();
        }
    }
}
