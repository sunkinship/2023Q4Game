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
    private float pauseCD = 0.2f;


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
            else if (inSubMenu == false)
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
            if (inSubMenu)
            {
                EventSystem.current.SetSelectedGameObject(optionsFirstSelect);
                MenuButton();
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
            if (GameManager.Instance.playerState == GameManager.State.play)
            {
                inputHandler.SwitchActionMap("Player");
            }
            else if (GameManager.Instance.playerState == GameManager.State.dialogue)
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
        TriggerFade();
        StartCoroutine(WaitForLoad(LoadMenuFromPause));
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
