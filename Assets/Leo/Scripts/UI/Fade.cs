using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [HideInInspector]
    private float transitionTime;

    public Animator transition;

    [HideInInspector]
    public bool done;

    private void Awake()
    {
        done = false;
    }

    public void TriggerFade()
    {
        StartCoroutine(TriggerFadeRoutine());   
    }

    private IEnumerator TriggerFadeRoutine()
    {
        done = false;

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        done = true;

        transition.SetTrigger("End");
    }

    public bool IsDone()
    {
        return done;
    }
}
