using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class UIController : MonoBehaviour
{
    [HideInInspector]
    protected PlayerInputHandler inputHandler;

    [Header("Fade Canvas")]
    [SerializeField]
    protected Fade fade;

    [Header("Button Indicator")]
    [SerializeField]
    protected GameObject indicators;

    [Header("General Menu Canvases")]
    [SerializeField] protected GameObject menu;
    [SerializeField] protected GameObject options;

    [Header("General First Select Buttons")]
    [SerializeField] protected GameObject menuFirstSelect;
    [SerializeField] protected GameObject optionsFirstSelect;

    protected GameObject lastSelected, lastLastSelected;

    protected bool inSubMenu;


    private void Start()
    {
        inputHandler = GameObject.FindGameObjectWithTag("Input").GetComponent<PlayerInputHandler>();
    }

    protected virtual void Update()
    {
        ReceiveCancelInput();
    }

    #region Button Methods
    public void OptionsButton()
    {
        inSubMenu = true;
        SetLastSelectedButton();
        menu.SetActive(false);
        options.SetActive(true);
        SetSelectedButton(optionsFirstSelect, true);
    }

    public virtual void MenuButton()
    {
        inSubMenu = false;
        SetLastSelectedButton();
        options.SetActive(false);
        menu.SetActive(true);
        SetSelectedButton(menuFirstSelect, false);
    }

    protected virtual void ReceiveCancelInput()
    {
        if (inputHandler.CancelInput && inSubMenu)
        {
            inputHandler.UseCancelInput();
            EventSystem.current.SetSelectedGameObject(optionsFirstSelect);
            MenuButton();
        }
    }
    #endregion

    #region Fade
    protected void TriggerFade() => fade.TriggerFade("StartQuickBlack", "EndQuickBlack");

    protected IEnumerator WaitForLoad(Func<bool> functionToCall)
    {
        while (fade.IsDone() == false)
        {
            yield return null;
        }
        functionToCall();
    }
    #endregion

    #region Selected GameObject
    protected void SetLastSelectedButton()
    {
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }

    protected void SetSelectedButton(GameObject deafultSelect, bool forceDefault)
    {
        if (forceDefault == false)
        {
            if (lastLastSelected != null)
            {
                EventSystem.current.SetSelectedGameObject(lastLastSelected);
                lastLastSelected = lastSelected;
                return;
            }
        }
        EventSystem.current.SetSelectedGameObject(deafultSelect);
        lastLastSelected = lastSelected;
    }
    #endregion
}
