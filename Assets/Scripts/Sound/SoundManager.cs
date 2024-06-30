using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] _soundClips;
    [SerializeField] private GameObject settingsScreen;

    public static SoundManager instance;
    public bool canPlay = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in _soundClips)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.gameObject.SetActive(true);
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string soundName)
    {
        if (!canPlay) return;

        Sound s = Array.Find(_soundClips, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Wrong sound name!");
            return;
        }
        s.source.Play();
    }
    public void OnOffSound()
    {
        Play("Button");
        canPlay = !canPlay;
    }
    public void OpenSettings()
    {
        Play("Button");
        settingsScreen.SetActive(true);
    }
    public void CloseSettings()
    {
        Play("Button");
        settingsScreen.SetActive(false);
    }
}
