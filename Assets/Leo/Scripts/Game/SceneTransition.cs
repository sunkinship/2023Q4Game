using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private bool isSecretExit;

    public void ChangeScenes()
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
}