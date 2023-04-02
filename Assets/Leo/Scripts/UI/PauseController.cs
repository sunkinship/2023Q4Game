using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : UIController
{
    [SerializeField]
    private GameObject pauseCanvas;
    [SerializeField]
    private PlayerInputHandler inputHandler;
    [SerializeField]
    private ControlBlur blur;
    private bool paused;


    private void Start()
    {
        paused = false;
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
        paused = true;
        //blur.EnableBlur();
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        //blur.DisableBlur();
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
