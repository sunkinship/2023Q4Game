using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [Header("Fade Canvas")]
    [SerializeField]
    protected Fade fade;

    [Header("Button Indicator")]
    [SerializeField]
    protected GameObject indicators;

    [Header("Menu Canvases")]
    [SerializeField] protected GameObject menu;
    [SerializeField] protected GameObject options;

    [Header("First Select Buttons")]
    [SerializeField] protected GameObject menuFirstSelect;
    [SerializeField] protected GameObject optionsFirstSelect;

    protected GameObject lastSelected, lastLastSelected;

    #region Button Methods
    public void OptionsButton()
    {
        SetLastSelectedButton();
        menu.SetActive(false);
        options.SetActive(true);
        SetSelectedButton(optionsFirstSelect);
    }

    public void MenuButton()
    {
        SetLastSelectedButton();
        options.SetActive(false);
        menu.SetActive(true);
        SetSelectedButton(menuFirstSelect);
    }
    #endregion

    #region Fade
    protected void TriggerFade() => fade.TriggerFade();

    protected IEnumerator WaitForLoad(Func<bool> sceneLoader)
    {
        while (fade.IsDone() == false)
        {
            yield return null;
        }
        sceneLoader();
    }
    #endregion

    #region Selected GameObject
    protected void SetLastSelectedButton()
    {
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }

    protected void SetSelectedButton(GameObject deafultSelect)
    {
        if (lastLastSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(lastLastSelected);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(deafultSelect);
        }
        lastLastSelected = lastSelected;
    }
    #endregion
}
