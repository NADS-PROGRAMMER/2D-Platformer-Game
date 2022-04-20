using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Sound[] sounds;
    public AudioSource currentBGMusic;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SetUpAllAudioClips();
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (this.currentBGMusic != null)
        {
            this.currentBGMusic.Stop();
        }

        if (scene.name == "Gameplay")
        {
            this.PlayBGMusic("AC3");
        }
        else if (scene.name == "Main Menu")
        {
            this.PlayBGMusic("Assassins Creed");
        }
        else if (scene.name == "Credits")
        {

        }
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


    /** Stop the all existing music. */
    private void StopAllMusic()
    {
        print("Stopping Music");
        foreach (Sound sound in sounds)
        {
            sound.audioSource.Stop();
        }
    }

    /* Play the sound by a specified name. 
       @param name
       - name of sound. */
    public void PlayBGMusic(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.audioName == name);
        this.currentBGMusic = sound.audioSource;
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
