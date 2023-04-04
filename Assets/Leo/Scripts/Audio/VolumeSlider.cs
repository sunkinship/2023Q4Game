using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;
    [Header("Volume Texts")]
    [SerializeField]
    private TextMeshProUGUI masterText;
    [SerializeField]
    private TextMeshProUGUI musicText;
    [SerializeField]
    private TextMeshProUGUI sfxText;

    private void Start()
    {
        InitializeSliders();
    }

    private void InitializeSliders()
    {
        SetMasterSlider();
        SetMusicSlider();
        SetSFXSlider();
    }

    #region Initialize Sliders
    private void SetMasterSlider()
    {
        float volume = PlayerPrefs.GetFloat("masterVolume");
        masterText.text = volume.ToString();
        masterSlider.value = volume;
        masterSlider.onValueChanged.AddListener(val => MasterVolumeChanged(val));
    }

    private void SetMusicSlider()
    {
        float volume = PlayerPrefs.GetFloat("musicVolume");
        musicText.text = volume.ToString();
        musicSlider.value = volume;
        musicSlider.onValueChanged.AddListener(val => MusicVolumeChanged(val));
    }

    private void SetSFXSlider()
    {
        float volume = PlayerPrefs.GetFloat("sfxVolume");
        sfxText.text = volume.ToString();
        sfxSlider.value = volume;
        sfxSlider.onValueChanged.AddListener(val => MusicSFXChanged(val));
    }
    #endregion

    #region Volume Changed
    private void MasterVolumeChanged(float volume)
    {
        masterText.text = volume.ToString();
        AudioManager.Instance.ChangeMasterVolume(volume);
    }

    private void MusicVolumeChanged(float volume)
    {
        musicText.text = volume.ToString();
        AudioManager.Instance.ChangeMusicVolume(volume);
    }

    private void MusicSFXChanged(float volume)
    {
        sfxText.text = volume.ToString();
        AudioManager.Instance.ChangeSFXVolume(volume);
    }
    #endregion
}