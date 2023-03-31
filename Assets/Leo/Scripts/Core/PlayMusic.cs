using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private AudioSource musicSource;
    [SerializeField] private AudioClip clip;
    public bool overrideVolume;

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
        musicSource.loop = true;
        musicSource.Play();
    }

    private void StopMusic()
    {
        musicSource.Stop();
    }

    public void TurnMusicOn()
    {
        //musicSource.volume = AudioManager.Instance.musicVol;
    }

    public void TurnMusicOff()
    {
        musicSource.volume = 0;
    }
}
