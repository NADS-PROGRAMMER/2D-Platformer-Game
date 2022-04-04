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

        if (onStartMusic != null)
        {
            this.Play(onStartMusic);
        }
    }

    // Start is called before the first frame update
    void Awake()
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

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.audioName == name);
        sound.audioSource.Play();
    }

    public void PlayDelayed(string name, float delay)
    {
        Sound sound = Array.Find(sounds, sound => sound.audioName == name);
        sound.audioSource.PlayDelayed(delay);
    }
}


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
