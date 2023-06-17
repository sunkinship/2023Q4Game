using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource musicSource, sfxSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loaded");
        musicSource = GameObject.FindGameObjectWithTag("Music Source").GetComponent<AudioSource>();
        sfxSource = GameObject.FindGameObjectWithTag("SFX Source").GetComponent<AudioSource>();

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    #region SFX
    public void PlaySFX(AudioClip clip) => sfxSource.PlayOneShot(clip);

    public void PlayerSFX(AudioClip clip, float volume) => sfxSource.PlayOneShot(clip, volume);

    public void StopSFX() => sfxSource.Stop();
    #endregion

    #region Music
    public void PlayMusic(AudioClip clip, bool canLoop)
    {
        musicSource.clip = clip;
        musicSource.loop = canLoop;
        musicSource.Play();
    }

    public void PlayMusic(AudioClip clip, bool canLoop, float volume)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = canLoop;
        musicSource.Play();
    }

    public void StopMusic() => musicSource.Stop();

    public void ResumeMusic() => musicSource.Play();
    #endregion

    #region Change Volume
    public void ChangeMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("masterVolume", volume);
        AudioListener.volume = volume * 0.1f;
    }

    public void ChangeMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("musicVolume", volume);
        musicSource.volume = volume * 0.1f;
    }

    public void ChangeSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("sfxVolume", volume);
        sfxSource.volume = volume * 0.1f;
    }
    #endregion

    #region Initialize Volume
    private void SetMasterVolume() => ChangeMasterVolume(PlayerPrefs.GetFloat("masterVolume"));

    private void SetMusicVolume() => ChangeMusicVolume(PlayerPrefs.GetFloat("musicVolume"));

    private void SetSFXVolume() => ChangeSFXVolume(PlayerPrefs.GetFloat("sfxVolume"));
    #endregion
}