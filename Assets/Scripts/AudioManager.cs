using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class to manage all audio sources.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    public AudioSource LowBeepSmooth;
    public AudioSource HighBeepSmooth;
    public AudioSource Bounce;
    public AudioSource Score;
}
