using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private GameObject music;
    private bool canPlay = true;

    private void Start()
    {
        music = FindObjectOfType<MusicController>().gameObject;
    }

    public void OnOffMusic()
    {
        SoundManager.instance.Play("Button");
        canPlay = !canPlay;

        if (canPlay)
        {
            music.SetActive(true);
        }
        else
        {
            music.SetActive(false);
        }
    }
}
