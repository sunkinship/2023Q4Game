using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Fade fade;

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

    public void LoadMenuFromPauseButton()
    {
        TriggerFade();
        StartCoroutine(WaitForLoad(LoadMenuFromPause));
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

    private bool LoadMenuFromPause()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        return true;
    }
    #endregion

    #region Fade
    private void TriggerFade() => fade.TriggerFade();

    private IEnumerator WaitForLoad(Func<bool> sceneLoader)
    {
        while (fade.IsDone() == false)
        {
            yield return null;
        }
        sceneLoader();
    }
    #endregion
}
