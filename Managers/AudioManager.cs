using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    public AudioSource SoundEffects;
    public AudioSource Music;

    [SerializeField] private AudioClip catchFishSound;
    [SerializeField] private AudioClip validWord;
    [SerializeField] private AudioClip invalidWord;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip bubbles;
    [SerializeField] private AudioClip backgroundAmbiance;
    
    private float volumeValue = 5.0f; // 0 to 10
    private const float ValueMid = 5.0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SoundEffects.volume = 0.7f * (volumeValue / ValueMid);
        PlayBackgroundAmbiance();
    }

    public void PlayFishCaughtSound()
    { 
        SoundEffects.PlayOneShot(catchFishSound);   
    }
    public void PlayValidWordSound()
    { 
        SoundEffects.PlayOneShot(validWord);   
    }
    public void PlayInvalidWordSound()
    { 
        SoundEffects.PlayOneShot(invalidWord);   
    }

    public void PlayBackgroundAmbiance()
    {
        Music.clip = backgroundAmbiance;
        Music.volume = 0.6f * ( volumeValue / ValueMid);
        Music.Play();
    }

    public void PauseMenuSoundsEnter()
    {
        Music.volume = 0.3f * ( volumeValue / ValueMid);
    }
    public void PauseMenuSoundsExit()
    {
        Music.volume = 0.6f * ( volumeValue / ValueMid);
    }

    public void PlayButtonClickSound()
    {
        SoundEffects.pitch = Random.Range(0.75f, 1);
        SoundEffects.PlayOneShot(buttonClick);
        SoundEffects.pitch = 1;
    }

    public void PlayFishBubbles()
    {
        SoundEffects.PlayOneShot(bubbles);   
    }

    public void onVolumeChange(float f)
    {
        volumeValue = f;
    }
    
}
