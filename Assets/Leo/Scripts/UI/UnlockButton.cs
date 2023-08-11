using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockButton : MonoBehaviour
{
    public string prefToCheck;

    public GameObject buttonLocked, buttonUnlocked;

    void Start()
    {
        CheckUnlock();
    }

    private void CheckUnlock()
    {
        if (PlayerPrefs.GetInt(prefToCheck) == 1)
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
