using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Sound[] sounds;
    public string onStartMusic;


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (onStartMusic != null)
        {
            this.Play(onStartMusic);
        }
    }


    void Awake()
    {
        SetUpAllAudioClips();
    }


    /* Add audio source to every audio clips in the
     * sounds array. */
    private void SetUpAllAudioClips()
    {
        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.isLoop;
        }
    }


    /* Play the sound by a specified name. 
       @param name
       - name of sound.
     */
    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.audioName == name);
        sound.audioSource.Play();
    }
}


/** This is the class of Sound */
[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string audioName;

    [Range(0, 1)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource audioSource;

    public bool isLoop;
}
