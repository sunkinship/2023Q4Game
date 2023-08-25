using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource musicSource, sfxSource;

    [HideInInspector] public bool persistMusic;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            GetAudioSources();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GetAudioSources()
    {
        musicSource = GameObject.FindGameObjectWithTag("Music Source").GetComponent<AudioSource>();
        sfxSource = GameObject.FindGameObjectWithTag("SFX Source").GetComponent<AudioSource>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
        if (persistMusic)
        {
            return;
        }
        musicSource.clip = clip;
        musicSource.loop = canLoop;
        musicSource.Play();
    }

    public void PlayMusic(AudioClip clip, bool canLoop, float volume)
    {
        if (persistMusic)
        {
            return;
        }
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = canLoop;
        musicSource.Play();
    }

    public void StopMusic() => musicSource.Stop();

    public void PauseMusic() => musicSource.Pause();

    public void ResumeMusic() => musicSource.Play();
    #endregion

    #region Transition
    //public void FadeOutMusic()
    //{
    //    StartCoroutine(WaitToFadeOut());   
    //}

    //private IEnumerator WaitToFadeOut()
    //{
    //    float elapsedTime = 0f;
    //    float percentageComplete;
    //    float startVolume = musicSource.volume;
    //    while (musicSource.volume > 0f)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        percentageComplete = elapsedTime / 0.5f;
    //        musicSource.volume = Mathf.Lerp(startVolume, 0f, percentageComplete);
    //        yield return null;
    //    }
    //}

    //private void FadeInMusic()
    //{
    //    musicSource.volume = 0;
    //    StartCoroutine(WaitToFadeIn());
    //}

    //private IEnumerator WaitToFadeIn()
    //{
    //    float elapsedTime = 0f;
    //    float percentageComplete;
    //    float targetVolume = PlayerPrefs.GetFloat("musicVolume") * 0.1f;
    //    while (musicSource.volume < targetVolume)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        percentageComplete = elapsedTime / 1;
    //        musicSource.volume = Mathf.Lerp(0, targetVolume, percentageComplete);
    //        yield return null;
    //    }
    //}
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