using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Static reference to the singleton instance
    private static AudioManager _instance;

    // Public accessor for the singleton instance
    public static AudioManager Instance
    {
        get
        {
            // Check if the instance has not been set yet
            if (_instance == null)
            {
                // Attempt to find an existing instance in the scene
                _instance = FindObjectOfType<AudioManager>();

                // If no instance exists, create a new one
                if (_instance == null)
                {
                    // Create a new GameObject to hold the GameManager instance
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound  s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void PlayAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

}
