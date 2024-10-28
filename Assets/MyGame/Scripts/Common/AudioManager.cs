using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("DangSon/AudioManager")]
public class AudioManager : MonoBehaviour
{
    
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get => instance;
    }
    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource sfxSourcePlayer;
    [Header("Audio BackGround")]
    public AudioClip backgroundMusic;
    private void Awake()
    {
        if (instance != null)
        {
        DestroyImmediate(gameObject);
        return;
        }
        instance = this;
    }
    private void Start()
    {
        PlayMusic(backgroundMusic);
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlaysfxPlayer(AudioClip clip) 
    {
       sfxSourcePlayer.PlayOneShot(clip);
    }
}
