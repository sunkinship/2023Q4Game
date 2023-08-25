using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFinalMusic : MonoBehaviour
{
    [SerializeField] private AudioClip finalSong;

    public void PlayMusic()
    {
        AudioManager.Instance.PlayMusic(finalSong, true);
        AudioManager.Instance.persistMusic = true;
    }
}
