using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_manager : MonoBehaviour
{
    public static audio_manager instance;
    public sound[] sounds;


    private List<string> clickSounds = new List<string>() {
        "planet_click1",
        "planet_click2",
        "planet_click3",
        "planet_click4",
    };

    private string lastTrackPlayed = "";
    private Coroutine musicCoroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeSounds();
        Play("main_theme");
    }

    private void OnDisable()
    {
        // Stop coroutine when script is disabled
        if (musicCoroutine != null)
        {
            StopCoroutine(musicCoroutine);
            musicCoroutine = null;
        }
    }

    private void InitializeSounds()
    {
        //init AudioSources for each sound
        foreach (sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.outputGroup;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            // Preload audio data to prevent lag spikes on first play
            if (s.clip != null)
            {
                s.clip.LoadAudioData();
            }
        }
    }

    public void Play(string name)
    {
        sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();
            //Debug.Log(s.outputGroup.name);
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void PlayVarryingPitch(string name, float minPitch, float maxPitch)
    {
        sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void PlayClick()
    {
       int maxBound = clickSounds.Count - 1;
       int rng = UnityEngine.Random.Range(0,maxBound);
       Play(clickSounds[rng]);
       //Debug.Log("played click");
    }

}
