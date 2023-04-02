using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    protected Fade fade;

    protected void TriggerFade() => fade.TriggerFade();

    protected IEnumerator WaitForLoad(Func<bool> sceneLoader)
    {
        while (fade.IsDone() == false)
        {
            yield return null;
        }
        sceneLoader();
    }
}
