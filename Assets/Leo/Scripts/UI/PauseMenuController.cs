using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuController : UIController
{
    [Header("Pause Canvas")]
    [SerializeField]
    private GameObject pauseCanvas;
    private bool paused, canUnpause;
    private readonly float pauseCD = 0.2f;


    protected override void Update()
    {
        ReceivePauseInput();
        if (paused)
            ReceiveCancelInput();
    }

    private void ReceivePauseInput()
    {
        if (inputHandler.PauseInput)
        {
            inputHandler.UsePauseInput();
            if (paused == false)
                PauseGame();
            else if (menuSubLevel == 0)
            {
                Unpause();
            }
        }
    }

    protected override void ReceiveCancelInput()
    {
        if (inputHandler.CancelInput)
        {
            inputHandler.UseCancelInput();
            if (menuSubLevel == 1)
            {
                EventSystem.current.SetSelectedGameObject(optionsFirstSelect);
                BackToMenu();
            }
            else
                Unpause();
        }
    }

    private void PauseGame()
    {
        canUnpause = false;
        StartCoroutine(WaitForPauseCD());
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        inputHandler.SwitchActionMap("UI");
        EventSystem.current.SetSelectedGameObject(menuFirstSelect);
        paused = true;
    }

    public void Unpause()
    {
        if (canUnpause)
        {
            if (GameManager.playerState == GameManager.PlayerState.play)
            {
                inputHandler.SwitchActionMap("Player");
            }
            else if (GameManager.playerState == GameManager.PlayerState.dialogue)
            {
                inputHandler.SwitchActionMap("Dialogue");
            }
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;
            paused = false;
        }
    }

    public void LoadMenuFromPauseButton()
    {
        TriggerFade(LoadMenuFromPause);
    }

    public bool LoadMenuFromPause()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        return true;
    }

    private IEnumerator WaitForPauseCD()
    {
        yield return new WaitForSecondsRealtime(pauseCD);
        canUnpause = true;
    }
}
