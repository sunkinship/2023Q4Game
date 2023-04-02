using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool toggleMaster, toggleMusic, toggleSFX;

    public void Toggle()
    {
        if (toggleMaster)
        {
            AudioManager.Instance.ToggleMaster();
        }
        else if (toggleSFX)
        {
            AudioManager.Instance.ToggleSFX();
        }
        else if (toggleMusic)
        {
            AudioManager.Instance.ToggleMusic();
        }
    }
}
