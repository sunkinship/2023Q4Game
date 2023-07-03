using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : UIController
{
    [Header("Other Canvases")]
    [SerializeField] 
    protected GameObject credits;
    [SerializeField]
    protected GameObject start;
    [SerializeField]
    protected GameObject freePlay;

    [Header("First Select Buttons")]
    [SerializeField]
    protected GameObject creditsFirstSelect;
    [SerializeField]
    protected GameObject startFirstSelect;
    [SerializeField]
    protected GameObject freePlayFirstSelect;


    #region Button Methods
    public void StartButton() => TriggerFade(LoadGame);

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

    public void QuitGameButton() => TriggerFade(QuitGame);
    #endregion

    #region Scene Loaders
    private bool LoadGame()
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