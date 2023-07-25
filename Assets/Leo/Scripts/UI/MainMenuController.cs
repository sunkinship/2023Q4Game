using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

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

    [Header("Free Play Buttons")]
    public GameObject freePlayButton;
    public GameObject level2;
    public GameObject level3;

    protected override void Start()
    {
        base.Start();
        if (gameState == GameState.free)
        {
            JumpToLevelSeclect();
        }
        ResetValues();
    }

    private void ResetValues()
    {
        abilityStateStory = 1;
    }

    #region Main Menu Buttons
    public void PlayButton()
    {
        menuSubLevel = 1;
        SetLastSelectedButton();
        menu.SetActive(false);
        play.SetActive(true);
        SetSelectedButton(playFirstSelect, true);
    }

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
    #endregion

    #region Play Menu Buttons
    public void NewGameButton()
    {
        Instance.SetGameStory();
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
    #endregion

    #region Free Play Menu Buttons
    public void Level1Button()
    {
        Instance.SetGameFree();
        TriggerFade(LoadLevel1);
    }

    public void Level2Button()
    {
        Instance.SetGameFree();
        TriggerFade(LoadLevel2);
    }

    public void Level3Button()
    {
        Instance.SetGameFree();
        TriggerFade(LoadLevel3);
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
        SceneManager.LoadScene(2);
        return true;
    }

    private bool LoadLevel1()
    {
        inputHandler.UseInteractInput();
        SceneManager.LoadScene(2);
        return true;
    }

    private bool LoadLevel2()
    {
        inputHandler.UseInteractInput();
        SceneManager.LoadScene(6);
        return true;
    }

    private bool LoadLevel3()
    {
        inputHandler.UseInteractInput();
        SceneManager.LoadScene(10);
        return true;
    }

    private bool QuitGame()
    {
        Application.Quit();
        return true;
    }
    #endregion

    #region Other
    private void JumpToLevelSeclect()
    {
        AddToButtonStack(menuFirstSelect);
        menu.SetActive(false);
        play.SetActive(true);
        AddToButtonStack(freePlayButton);
        play.SetActive(false);
        free.SetActive(true);
        menuSubLevel = 2;
        SetLastSelectedButton(free);
        if (currentLevel == 1)
        {
            SelectButton(freeFirstSelect);
        }
        else if (currentLevel == 2)
        {
            SelectButton(level2);
        }
        else if (currentLevel == 3)
        {
            SelectButton(level3);
        }
    }
    #endregion
}