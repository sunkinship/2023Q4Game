using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : UIController
{

    #region Button Methods
    public void LoadGameButton()
    {
        TriggerFade();
        StartCoroutine(WaitForLoad(LoadGame));
    }

    public void LoadCreditsButton()
    {
        TriggerFade();
        StartCoroutine(WaitForLoad(LoadCredits));
    }

    public void LoadMenuButton()
    {
        TriggerFade();
        StartCoroutine(WaitForLoad(LoadMenu));
    }

    public void QuitGameButton()
    {
        TriggerFade();
        StartCoroutine(WaitForLoad(QuitGame));
    }
    #endregion

    #region SceneLoaders
    private bool LoadGame()
    {
        SceneManager.LoadScene(1);
        return true;
    }

    private bool LoadCredits()
    {
        SceneManager.LoadScene(2);
        return true;
    }

    private bool LoadMenu()
    {
        SceneManager.LoadScene(0);
        return true;
    }

    private bool QuitGame()
    {
        Application.Quit();
        return true;
    }
    #endregion
}
