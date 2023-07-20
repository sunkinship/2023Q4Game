using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockFreePlay : MonoBehaviour
{
    public GameObject buttonLocked, buttonUnlocked;

    void Start()
    {
        CheckUnlock();
    }

    private void CheckUnlock()
    {
        if (PlayerPrefs.GetInt("beatGame") == 1)
        {
            buttonLocked.SetActive(false);
            buttonUnlocked.SetActive(true);
        }
        else
        {
            buttonLocked.SetActive(true);
            buttonUnlocked.SetActive(false);
        }
    }
}
