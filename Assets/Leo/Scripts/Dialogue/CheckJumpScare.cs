using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckJumpScare : MonoBehaviour
{
    public void FinishedJumpScare()
    {
        AudioManager.Instance.StopMusic();
        Transition.Instance.TriggerFadeBoth("StartBlackScreen", "EndLongBlack", GameManager.Instance.LoadMainMenu);
    }
}
