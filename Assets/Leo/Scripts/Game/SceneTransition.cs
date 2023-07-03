using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private bool isSecretExit;

    public void ChangeScenes()
    {
        if (isSecretExit)
        {
            Transition.Instance.TriggerFadeBoth("StartLongWhite", "EndLongWhite", GameManager.Instance.LoadNextNormalScene);
        }
        else
        {
            Transition.Instance.TriggerFadeBoth("StartLongWhite", "EndLongWhite", GameManager.Instance.LoadNextNormalScene);
        }
    }
}