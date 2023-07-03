using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public State playerState;

    public enum State 
    {
        play, dialogue, ui
    }

    [HideInInspector]
    public bool blackFade;

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
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log("loaded");
    //}

    #region Change Action
    public void SetPlayAction()
    {   
        playerState = State.play;
        PlayerInputHandler.Instance.SwitchActionMap("Player");
    }

    public void SetDialogueAction()
    {
        playerState = State.dialogue;
        PlayerInputHandler.Instance.SwitchActionMap("Dialogue");
    }

    public void SetUIAction()
    {
        playerState = State.ui;
        PlayerInputHandler.Instance.SwitchActionMap("UI");
    }
    #endregion

    #region Load Scenes
    public bool LoadNextNormalScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        return true;
    }

    public bool LoadNextSecretScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        return true;
    }

    public bool LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        return true;
    }
    #endregion
}