using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public PlayerState playerState;
    [HideInInspector]
    public GameState gameState; 


    public enum PlayerState 
    {
        play, dialogue, ui
    }

    public enum GameState
    {
        story, free
    }

    [HideInInspector]
    public bool blackFade;

    private int currentLevel;

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
    //    Debug.Log("loaded");
    //}

    #region Change Player Action
    public void SetActionPlay()
    {   
        playerState = PlayerState.play;
        PlayerInputHandler.Instance.SwitchActionMap("Player");
    }

    public void SetActionDialogue()
    {
        playerState = PlayerState.dialogue;
        PlayerInputHandler.Instance.SwitchActionMap("Dialogue");
    }

    public void SetActionUI()
    {
        playerState = PlayerState.ui;
        PlayerInputHandler.Instance.SwitchActionMap("UI");
    }
    #endregion

    #region Set Game State
    public void SetGameStory() => gameState = GameState.story;

    public void SetGameFree() => gameState = GameState.free;
    #endregion

    #region Load Scenes
    public bool LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        return true;
    }

    public bool LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        return true;
    }

    public bool LoadNextNextNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        return true;
    }
    #endregion
}