using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    //Holds if the game is paused
    public static bool gameIsPaused = false;

    //Holds wanted blur value
    public float wantedBlur = 1.37f;

    public float wantedPosX = 21.1f;
    public Vector2 defaultx = new Vector2(-100, 26.2f);

    public GameObject buttonHolder;  //Holds holder for pause menu buttons
    public GameObject[] buttons; //Holds buttons

    Image bI;
    Image[] bIS = new Image[3];

    private void Start()
    {
        gameIsPaused = false;

        bI = buttonHolder.GetComponent<Image>();

        for (int i = 0; i < 3; i++)
        {
            bIS[i] = buttons[i].GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for Cancel input
        if (Input.GetButtonDown("Cancel"))
        {
            //if the game is paused
            if (gameIsPaused)
            {
                //Calls Resume method
                Resume();
            }
            else
            {
                //Calls Pause method
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(buttons[0]);
                Pause();
            }
        }
    }

    //Resumes game
    public void Resume()
    {
            //Start sound up again
            AudioListener.pause = false;

            buttonHolder.SetActive(false);

            bI.color = new Color(bI.color.r, bI.color.g, bI.color.b, 0);

            for (int i = 0; i < 3; i++)
            {
                bIS[i].color = new Color(bIS[i].color.r, bIS[i].color.g, bIS[i].color.b, 0);
            }

            //Turn on time
            Time.timeScale = 1f;

            //Sets gameIsPaused to false
            gameIsPaused = false;
    }

    //Pauses game
    void Pause()
    {
            //Pause sound
            AudioListener.pause = true;

            buttonHolder.SetActive(true);

            StartCoroutine(fadeIn());

            //Sets gameIsPaused to true
            gameIsPaused = true;
    }

    //Go to menu
    public void LoadMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
    }

    //Go to menu
    public void LoadCredits() {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene(2);
    }

    //Quit game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Would of quit if built");
    }

    private IEnumerator fadeIn()
    {
        for (float i = 0; i < 1; i += 0.05f)
        {

            bI.color = new Color(bI.color.r, bI.color.g, bI.color.b, i); 

            for (int j = 0; j < 3; j++)
            {
                bIS[j].color = new Color(bIS[j].color.r, bIS[j].color.g, bIS[j].color.b, i);
            }

            yield return new WaitForFixedUpdate();
        }

        bI.color = Color.white;

        for (int j = 0; j < 3; j++)
        {
            bIS[j].color = Color.white;
        }

        //Turn off time
        Time.timeScale = 0f;
    }
}
