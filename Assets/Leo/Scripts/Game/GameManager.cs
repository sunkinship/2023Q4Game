using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public State playerState;

    public enum State 
    {
        play, dialogue, ui
    }

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
    public void LoadNextCutScene(bool isSecretExit)
    {
        if (isSecretExit)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
}