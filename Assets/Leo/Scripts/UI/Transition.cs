using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
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
            //SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    print("loaded scene");
    //}

    private void Start()
    {
        ani.SetTrigger("EndLongBlack");
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
        StartCoroutine(WaitForFade(end, functionToCall));
    }

    private IEnumerator WaitForFade(string end, Func<bool> functionToCall)
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

    public void ResetFadeEnd() => fadeEndFinished = false;

    public bool GetFadeEnd()
    {
        return fadeEndFinished;
    }
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