using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private bool isSecretExit, giveAbility, isTutorial, isIntro;
    

    public void ChangeScenes()
    {
        if (CheckForOverride())
            return;

        if (isSecretExit)
        {
            if (GameManager.gameState == GameManager.GameState.free)
            {
                Transition.Instance.TriggerFadeBoth("StartLongWhite", "EndLongWhite", GameManager.Instance.LoadMainMenu);
            }
            else
            {
                IncreaseAbilityValues();
                Transition.Instance.TriggerFadeBoth("StartLongWhite", "EndLongWhite", GameManager.Instance.LoadNextNextNextScene);
            }
        }
        else
        {
            if (GameManager.gameState == GameManager.GameState.free)
            {
                Transition.Instance.TriggerFadeBoth("StartLongBlack", "EndLongBlack", GameManager.Instance.LoadMainMenu);
            }
            else
            {
                Transition.Instance.TriggerFadeBoth("StartLongBlack", "EndLongBlack", GameManager.Instance.LoadNextScene);
            }
        }
    }

    private bool CheckForOverride()
    {
        if (isTutorial)
        {
            Transition.Instance.TriggerFadeBoth("StartLongWhite", "EndLongWhite", GameManager.Instance.LoadNextScene);
            return true;
        }
        else if (isIntro)
        {
            Transition.Instance.TriggerFadeBoth("StartLongBlack", "EndLongBlack", GameManager.Instance.LoadNextScene);
            return true;
        }
        return false;
    }

    private void IncreaseAbilityValues()
    {
        if (giveAbility) 
        {
            GameManager.abilityStateStory++;
            if (GameManager.GetAbilityState() > 2)
                return;
            GameManager.IncreaseAbilityState();
        }
    }
}