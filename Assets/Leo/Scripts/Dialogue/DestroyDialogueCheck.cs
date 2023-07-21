using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDialogueCheck : MonoBehaviour
{
    void Start()
    {
        if (GameManager.gameState != GameManager.GameState.story)
        {
            Destroy(gameObject);
        }
    }
}
