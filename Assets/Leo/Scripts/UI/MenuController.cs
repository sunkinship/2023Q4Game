using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //loads main scene
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    //loads credits scene
    public void CreditsButton()
    {
        SceneManager.LoadScene(2);
    }

    //loads main menu scene
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    //loads main menu and unpauses game
    public void QuitToMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    //quit game
    public void QuitGameButton()
    {
        Application.Quit();
    }
}
