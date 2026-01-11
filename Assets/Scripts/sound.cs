using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class sound
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup outputGroup;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop;
    //TODON MAKE DISABLED WORK
    public bool disabled = false;
    [HideInInspector]
    public AudioSource source;

}
