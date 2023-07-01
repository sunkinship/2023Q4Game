using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public static Transition Instance;

    [SerializeField]
    private Animator ani;

    private bool isFadeFinished;


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
    public void TriggerFadeStart(string start)
    {
        ani.SetTrigger(start);
    }

    public void TriggerFadeEnd(string end)
    {
        ani.SetTrigger(end);
    }

    public void TriggerFadeBoth(string start, string end, Func<bool> functionToCall)
    {
        ani.SetTrigger(start);
        isFadeFinished = false;
        StartCoroutine(WaitForFade(end, functionToCall));
    }

    private IEnumerator WaitForFade(string end, Func<bool> functionToCall)
    {
        while (isFadeFinished == false)
            yield return null;
        functionToCall();
        ani.SetTrigger(end);
    }

    public void AnimationFinishTrigger() => isFadeFinished = true;
    #endregion
}