using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource effectsSource;
    public AudioSource musicSource;

    [SerializeField] private bool isMainMenu;
    [SerializeField] private bool playBuildUp;
    [SerializeField] private bool playGameMusic;
    
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private float mainMenuMusicDelay;
    [SerializeField] private AudioClip introMusic;
    [SerializeField] private AudioClip buildUpMusic;
    [SerializeField] private AudioClip gameMusic;
    
    public static SoundManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        if (isMainMenu)
        {
            PlayMusic(mainMenuMusic, (mainMenuMusicDelay));
        }
        else
        {
            PlayMusic(introMusic, (ulong)0f);
        }
    }

    private void Update()
    {
        if (!isMainMenu)
        {
            if (playBuildUp)
            {
                playBuildUp =  false;
                PlayMusic(buildUpMusic, (ulong)0f);
            }

            if (playGameMusic)
            {
                playGameMusic =  false;   
                PlayMusic(gameMusic, (ulong)0f);
            }
        }
    }

    public void SetVolume(Slider slider)
    {
        musicSource.volume = slider.value;
    }
    
    public void Play(AudioClip clip, float delay)
    {
        effectsSource.clip = clip;
        effectsSource.PlayDelayed(delay);
    }
    
    public void PlayMusic(AudioClip clip, float delay)
    {
        musicSource.clip = clip;
        musicSource.PlayDelayed(delay);
        
    }
}
