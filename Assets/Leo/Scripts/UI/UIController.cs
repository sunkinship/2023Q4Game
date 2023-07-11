using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [HideInInspector]
    protected PlayerInputHandler inputHandler;

    [Header("Button Indicator")]
    [SerializeField]
    protected GameObject indicators;

    [Header("General Menus")]
    [SerializeField] protected GameObject menu;
    [SerializeField] protected GameObject options;

    [Header("General First Select Buttons")]
    [SerializeField] protected GameObject menuFirstSelect;
    [SerializeField] protected GameObject optionsFirstSelect;

    protected GameObject mostRecentlySelected;
    protected Stack previouslySelected = new Stack();

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

    #region Selected Buttons
    protected void SetLastSelectedButton()
    {
        mostRecentlySelected = EventSystem.current.currentSelectedGameObject;
    }

    //protected void SetSelectedButton(GameObject deafultSelect, bool forceDefault)
    //{
    //    if (forceDefault == false)
    //    {
    //        if (lastLastSelected != null)
    //        {
    //            EventSystem.current.SetSelectedGameObject(lastLastSelected);
    //            lastLastSelected = mostRecentlySelected;
    //            return;
    //        }
    //    }
    //    EventSystem.current.SetSelectedGameObject(deafultSelect);
    //    lastLastSelected = mostRecentlySelected;
    //}

    protected void SetSelectedButton(GameObject deafultSelect, bool forceDefault)
    {
        if (forceDefault == false)
        {
            if (previouslySelected.Count > 0)
            {
                EventSystem.current.SetSelectedGameObject((GameObject)previouslySelected.Pop());
                return;
            }
        }
        EventSystem.current.SetSelectedGameObject(deafultSelect);
        previouslySelected.Push(mostRecentlySelected); 
    }
    #endregion

    #region Fade
    protected void TriggerFade(Func<bool> functionToCall) => Transition.Instance.TriggerFadeBoth("StartLongBlack", "EndLongBlack", functionToCall);
    #endregion
}