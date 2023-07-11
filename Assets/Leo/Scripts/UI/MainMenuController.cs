using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : UIController
{
    [Header("Specific Menus")]
    public GameObject credits;
    public GameObject play;
    public GameObject free;

    [Header("First Select Buttons")]
    public GameObject creditsFirstSelect;
    public GameObject playFirstSelect;
    public GameObject freeFirstSelect;


    #region Main Menu Buttons
    public void PlayButton()
    {
        menuSubLevel = 1;
        SetLastSelectedButton();
        menu.SetActive(false);
        play.SetActive(true);
        SetSelectedButton(playFirstSelect, true);
    }

    #region Play Menu Buttons
    public void NewGameButton()
    {
        GameManager.Instance.SetGameStory();
        TriggerFade(LoadNewGame);
    }

    public void FreePlayButton()
    {
        menuSubLevel = 2;
        SetLastSelectedButton();
        play.SetActive(false);
        free.SetActive(true);
        SetSelectedButton(freeFirstSelect, true);
    }

    public void BackToPlayMenu()
    {
        menuSubLevel = 1;
        SetLastSelectedButton();
        free.SetActive(false);
        play.SetActive(true);
        SetSelectedButton(playFirstSelect, false);
    }

    #region Free Play Menu Buttons
    public void Level1Button()
    {
        GameManager.Instance.SetGameFree();
        TriggerFade(LoadLevel1);
    }

    public void Level2Button()
    {
        GameManager.Instance.SetGameFree();
        TriggerFade(LoadLevel2);
    }

    public void Level3Button()
    {
        GameManager.Instance.SetGameFree();
        TriggerFade(LoadLevel3);
    }
    #endregion

    #endregion

    public void CreditsButton()
    {
        menuSubLevel = 1;
        SetLastSelectedButton();
        menu.SetActive(false);
        credits.SetActive(true);
        SetSelectedButton(creditsFirstSelect, true);
    }

    public override void BackToMenu()
    {
        menuSubLevel = 0;
        SetLastSelectedButton();
        options.SetActive(false);
        credits.SetActive(false);
        play.SetActive(false);
        menu.SetActive(true);
        SetSelectedButton(menuFirstSelect, false);
    }

    public void QuitGameButton() => TriggerFade(QuitGame);

    public void JumpToFreeMenu()
    {
        PlayButton();
        FreePlayButton();
    }
    #endregion

    #region Cancel
    protected override void ReceiveCancelInput()
    {
        if (inputHandler.CancelInput)
        {
            inputHandler.UseCancelInput();
            if (menuSubLevel == 0)
                return;
            else if (menuSubLevel == 1)
                BackToMenu();
            else if (menuSubLevel == 2)
                BackToPlayMenu();
        }
    }
    #endregion  

    #region Scene Loaders
    private bool LoadNewGame()
    {
        inputHandler.UseInteractInput();
        SceneManager.LoadScene(3);
        return true;
    }

    private bool LoadLevel1()
    {
        inputHandler.UseInteractInput();
        SceneManager.LoadScene(3);
        return true;
    }

    private bool LoadLevel2()
    {
        inputHandler.UseInteractInput();
        SceneManager.LoadScene(3);
        return true;
    }

    private bool LoadLevel3()
    {
        inputHandler.UseInteractInput();
        SceneManager.LoadScene(3);
        return true;
    }

    private bool QuitGame()
    {
        Application.Quit();
        return true;
    }
    #endregion
}