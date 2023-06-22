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
        GameManager.Instance.LoadNextCutScene(isSecretExit);
    }
}