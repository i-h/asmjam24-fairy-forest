using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class SoundManager : MonoBehaviour
{
    [SerializeField] private float changeDuration = 1.0f;
    private float changeStart = -1;
    public AudioSource backgroundMusic1;
    public AudioSource backgroundMusic2;
    private bool isPlayingMusic1;

    void Start()
    {
        // Alkuarvot: soitetaan ensimmäistä taustamusiikkia
        isPlayingMusic1 = true;
        UpdateMusicVolumes();
        OtherSideManager.WorldChanged += OnWorldChanged;
    }
    private void OnDestroy(){
        
        OtherSideManager.WorldChanged -= OnWorldChanged;
    }

    

    private void OnWorldChanged(OtherSideManager.World world)
    {
        ToggleMusic(world == OtherSideManager.World.Normal);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMusic(!isPlayingMusic1);
        }
        if(backgroundMusic1.volume != (isPlayingMusic1 ? 1f : 0f)){
            UpdateMusicVolumes();
        }
    }

    void UpdateMusicVolumes()
    {
        float t = Mathf.Clamp01((Time.unscaledTime - changeStart) / changeDuration);
        Debug.Log(t);
        if (isPlayingMusic1)
        {
            backgroundMusic1.volume = t;
            backgroundMusic2.volume = 1f-t;
        }
        else
        {
            backgroundMusic1.volume = 1f-t;
            backgroundMusic2.volume = t;
        }
    }

    public void ToggleMusic(bool music1)
    {
        isPlayingMusic1 = music1;
        changeStart = Time.unscaledTime;
        UpdateMusicVolumes();
    }
}