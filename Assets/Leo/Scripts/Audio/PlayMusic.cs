using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioClip musicTrack;

    void Start()
    {
        AudioManager.Instance.PlayMusic(musicTrack, true);
        Destroy(gameObject);
    }
}
