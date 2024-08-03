using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundMusic1;
    public AudioSource backgroundMusic2;
    private bool isPlayingMusic1;

    void Start()
    {
        // Alkuarvot: soitetaan ensimmäistä taustamusiikkia
        isPlayingMusic1 = true;
        UpdateMusicVolumes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMusic();
        }
    }

    void UpdateMusicVolumes()
    {
      
    



        if (isPlayingMusic1)
        {
            backgroundMusic1.volume = 1.0f;
            backgroundMusic2.volume = 0.0f;
        }
        else
        {
            backgroundMusic1.volume = 0.0f;
            backgroundMusic2.volume = 1.0f;
        }
    }

    public void ToggleMusic()
    {
        isPlayingMusic1 = !isPlayingMusic1;
        UpdateMusicVolumes();
    }
}