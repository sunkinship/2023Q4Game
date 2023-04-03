using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : UIController
{
    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject options;
    [Header("First Select Buttons")]
    [SerializeField] private GameObject menuFirstSelect;
    [SerializeField] private GameObject optionsFirstSelect;
    [Header("Last Selected Checker")]
    [SerializeField] private GameObject lastSelected;
    [SerializeField] private GameObject currentSelected;

    #region Button Methods
    public void StartButton()
    {
        TriggerFade();
        StartCoroutine(WaitForLoad(LoadGame));
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsFirstSelect);
    }

    public void MenuButton()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(menuFirstSelect);
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

    private void SetLastSelectedButton()
    {
        if (EventSystem.current.currentSelectedGameObject != currentSelected)
        {
            lastSelected = currentSelected;
            currentSelected = EventSystem.current.currentSelectedGameObject;
        }
    }
}