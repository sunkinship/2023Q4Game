using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public static Transition Instance;

    [SerializeField]
    private Animator ani;

    private bool fadeStartFinished, fadeEndFinished;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("loaded");
    }

    #region Fade Animation
    public void TriggerFadeBoth(string start, string end, Func<bool> functionToCall)
    { 
        DisableInput();
        ani.SetTrigger(start);
        fadeStartFinished = false;
        fadeEndFinished = false;
        StartCoroutine(WaitForFade(end, functionToCall));
    }

    public void TriggerFadeBothDeath(string start, string end, Func<bool> functionToCall)
    {
        DisableInput();
        ani.SetTrigger(start);
        fadeStartFinished = false;
        fadeEndFinished = false;
        StartCoroutine(WaitForFadeDeath(end, functionToCall));
    }

    private IEnumerator WaitForFade(string end, Func<bool> functionToCall)
    {
        while (fadeStartFinished == false)
            yield return null;
        functionToCall();
        ani.SetTrigger(end);
        while (fadeEndFinished == false)
            yield return null;
        GameManager.Instance.SetActionAfterLoad();
    }

    private IEnumerator WaitForFadeDeath(string end, Func<bool> functionToCall)
    {
        while (fadeStartFinished == false)
            yield return null;
        functionToCall();
        ani.SetTrigger(end);
        while (fadeEndFinished == false)
            yield return null;
        EnableInput();
    }

    public void AnimationStartFinishTrigger() => fadeStartFinished = true;

    public void AnimationEndFinishTrigger() => fadeEndFinished = true;
    #endregion

    #region Input Control
    private void EnableInput()
    {
        PlayerInputHandler.Instance.EnableInput();
    }

    private void DisableInput()
    {
        PlayerInputHandler.Instance.DisableInput();
    }
    #endregion
}