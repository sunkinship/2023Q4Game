using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private AudioSource musicSource;
    [SerializeField] 
    private AudioClip clip;
    [SerializeField]
    private float musicVol;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Play(clip);
    }

    private void Play(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.volume = musicVol;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ResumeMusic()
    {
        musicSource.Play();
    }

    public void MuteMusic()
    {
        musicSource.volume = 0;
    }
}
