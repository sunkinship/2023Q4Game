using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [HideInInspector]
    protected PlayerInputHandler inputHandler;

    [Header("Audio Clips")]
    [SerializeField] protected AudioClip selectClip;
    [SerializeField] protected AudioClip changeClip;

    [Header("General Menus")]
    [SerializeField] protected GameObject menu;
    [SerializeField] protected GameObject options;

    [Header("General First Select Buttons")]
    [SerializeField] protected GameObject menuFirstSelect;
    [SerializeField] protected GameObject optionsFirstSelect;

    protected GameObject mostRecentlySelected;
    protected Stack previouslySelected = new Stack();

    protected int menuSubLevel = 0;


    protected virtual void Start()
    {
        inputHandler = GameObject.FindGameObjectWithTag("Input").GetComponent<PlayerInputHandler>();
    }

    protected virtual void Update()
    {
        ReceiveCancelInput();
        //print(EventSystem.current.currentSelectedGameObject.name);
    }

    #region Button Methods
    public void OptionsButton()
    {
        menuSubLevel = 1;
        SetLastSelectedButton();
        menu.SetActive(false);
        options.SetActive(true);
        SetSelectedButton(optionsFirstSelect, true);
    }

    public virtual void BackToMenu()
    {
        menuSubLevel = 0;
        SetLastSelectedButton();
        options.SetActive(false);
        menu.SetActive(true);
        SetSelectedButton(menuFirstSelect, false);
    }
    #endregion

    #region Cancel
    protected virtual void ReceiveCancelInput()
    {
        if (inputHandler.CancelInput && menuSubLevel == 1)
        {
            inputHandler.UseCancelInput();
            //EventSystem.current.SetSelectedGameObject(optionsFirstSelect);
            BackToMenu();
        }
    }
    #endregion  

    #region Selected Buttons
    protected void SetLastSelectedButton()
    {
        mostRecentlySelected = EventSystem.current.currentSelectedGameObject;
    }

    protected void SetLastSelectedButton(GameObject button)
    {
        mostRecentlySelected = button;
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

    protected void SelectButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(button);
    }

    protected void AddToButtonStack(GameObject button)
    {
        previouslySelected.Push(button);
    }
    #endregion

    #region Fade
    protected void TriggerFade(Func<bool> functionToCall) => Transition.Instance.TriggerFadeBoth("StartLongBlack", "EndLongBlack", functionToCall);
    #endregion

    #region Audio 
    public void OnSelectAudio()
    {
        AudioManager.Instance.PlaySFX(selectClip);
    }

    public void OnChangeAudio()
    {
        AudioManager.Instance.PlaySFX(changeClip);
    }
    #endregion
}