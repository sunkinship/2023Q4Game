using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuController : UIController
{
    [Header("Pause Menu")]
    [SerializeField]
    private GameObject pauseCanvas;
    [SerializeField]
    private PlayerInputHandler inputHandler;
    private bool paused;


    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(menuFirstSelect);
    }

    private void Update()
    {
        ReceivePauseInput();
    }

    private void ReceivePauseInput()
    {
        if (inputHandler.PauseInput)
        {
            inputHandler.UsePauseInput();
            if (paused == false)
                PauseGame();
            else
                Unpause();
        }
    }

    private void PauseGame()
    {
        inputHandler.SwitchActionMap("UI");
        paused = true;
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
    }

    public void Unpause()
    {
        inputHandler.SwitchActionMap("Player");
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        paused = false;
    }

    public void LoadMenuFromPauseButton()
    {
        TriggerFade();
        StartCoroutine(WaitForLoad(LoadMenuFromPause));
    }

    public bool LoadMenuFromPause()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        return true;
    }
}
