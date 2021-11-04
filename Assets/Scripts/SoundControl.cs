using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{

    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    public AudioSource myAudioSource1;
    public AudioSource myAudioSource2;
    public AudioSource myAudioSource3;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource1 = AddAudio (true, false, 0.2f);
        myAudioSource1.clip = audioClip1;
        myAudioSource2.clip = audioClip2;
        myAudioSource2.clip = audioClip3;
        myAudioSource2.volume = 0.15f;

    }
    public AudioSource AddAudio(bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        //newAudio.clip = clip; 
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }

    public void assignClips()
    {
        myAudioSource2.clip = audioClip2;
    }
    public void PlaySound1()
    {
        myAudioSource1.Play();
        //and so on
    }
    public void PlaySound2()
    {
        myAudioSource2.PlayOneShot(audioClip2);
    }
    public void StopSound1()
    {
        myAudioSource1.Stop();
    }

    public void PlaySoundClip3()
    {
        myAudioSource3.PlayOneShot(audioClip3);
    }
}
