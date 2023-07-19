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
            if (GameManager.Instance.gameState != GameManager.GameState.story)
            {
                Transition.Instance.TriggerFadeBoth("StartLongWhite", "EndLongWhite", GameManager.Instance.LoadMainMenu);
            }
            else
            {
                Transition.Instance.TriggerFadeBoth("StartLongWhite", "EndLongWhite", GameManager.Instance.LoadNextNextNextScene);
            }
        }
        else
        {
            if (GameManager.Instance.gameState != GameManager.GameState.story)
            {
                Transition.Instance.TriggerFadeBoth("StartLongBlack", "EndLongBlack", GameManager.Instance.LoadMainMenu);
            }
            else
            {
                Transition.Instance.TriggerFadeBoth("StartLongBlack", "EndLongBlack", GameManager.Instance.LoadNextScene);
            }
        }
    }
}