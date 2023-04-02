using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] 
    private Slider slider;
    [SerializeField] 
    private bool master, music, sfx;
    private float masterVolume, musicVolume, sfxVolume;

    private void Start()
    {
        if (master)
        {
            slider.value = PlayerPrefs.GetFloat("masterVolume");
            AudioManager.Instance.ChangeMasterVolume(slider.value);
            slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeMasterVolume(val));
        }
        else if (music)
        {
            slider.value = PlayerPrefs.GetFloat("musicVolume");
            AudioManager.Instance.ChangeMusicVolume(slider.value);
            slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeMusicVolume(val));
        }
        else if (sfx)
        {
            slider.value = PlayerPrefs.GetFloat("sfxVolume");
            AudioManager.Instance.ChangeSFXVolume(slider.value);
            slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeSFXVolume(val));
        }
    }

    private void Update()
    {
        if (master)
        {
            masterVolume = slider.value;
            PlayerPrefs.SetFloat("masterVolume", masterVolume);
        }
        if (music)
        {
            musicVolume = slider.value;
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
        }
        if (sfx)
        {
            sfxVolume = slider.value;
            PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        }
    }
}