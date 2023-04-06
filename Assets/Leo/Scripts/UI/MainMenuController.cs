using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : UIController
{
    [Header("Credits Canvas")]
    [SerializeField] protected GameObject credits;

    [Header("First Select Buttons")]
    [SerializeField] protected GameObject creditsFirstSelect;

    #region Button Methods
    public void StartButton()
    {
        TriggerFade();
        StartCoroutine(WaitForLoad(LoadGame));
    }

    public void CreditsButton()
    {
        inSubMenu = true;
        SetLastSelectedButton();
        menu.SetActive(false);
        credits.SetActive(true);
        SetSelectedButton(creditsFirstSelect, true);
    }

    public override void MenuButton()
    {
        inSubMenu = false;
        SetLastSelectedButton();
        options.SetActive(false);
        credits.SetActive(false);
        menu.SetActive(true);
        SetSelectedButton(menuFirstSelect, false);
    }

    public void QuitGameButton()
    {
        TriggerFade();
        StartCoroutine(WaitForLoad(QuitGame));
    }
    #endregion

    #region Scene Loaders
    private bool LoadGame()
    {
        SceneManager.LoadScene(1);
        return true;
    }

    private bool QuitGame()
    {
        Application.Quit();
        return true;
    }
    #endregion
}