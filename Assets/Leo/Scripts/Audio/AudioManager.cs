using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] 
    private AudioSource musicSource, sfxSource;

    private bool masterMuted;

    [SerializeField]
    private Slider masterSlider;

    [SerializeField]
    private TextMeshProUGUI masterText, musicText, sfxText;

    public void Awake()
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
        WaitToAssign();
    }

    private void WaitToAssign()
    {
        //Debug.Log("Scene loaded");
        /*masterSlider = GameObject.Find("Master Slider").GetComponent<Slider>();
        masterText = GameObject.Find("MasterText").GetComponent<TextMeshProUGUI>();
        musicText = GameObject.Find("MusicText").GetComponent<TextMeshProUGUI>();
        sfxText = GameObject.Find("SFXText").GetComponent<TextMeshProUGUI>();*/
    }

    #region Play Audio
    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void StopSound()
    {
        sfxSource.Stop();
    }

    public void PlayMusic(AudioClip clip, float volume)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
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
    #endregion


    #region Volume
    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        masterText.text = "Master Volume: " + (int)(AudioListener.volume * 100);
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicText.text = "Music Volume: " + (int)(musicSource.volume * 100);
    }

    public void ChangeSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        sfxText.text = "SFX Volume: " + (int)(sfxSource.volume * 100);
    }
    #endregion


    #region Toggle Mute
    public void ToggleMaster()
    {
        if (masterMuted == false)
        {
            masterMuted = true;
            AudioListener.volume = 0;
        }
        else if (masterMuted)
        {
            masterMuted = false;
            AudioListener.volume = masterSlider.value;
        }
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    #endregion
}
