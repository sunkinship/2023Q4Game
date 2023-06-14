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

    public void TriggerFade(string start, string end)
    {
        StartCoroutine(TriggerFadeRoutine(start, end));
    }

    private IEnumerator TriggerFadeRoutine(string start, string end)
    {
        done = false;

        transition.SetTrigger(start);

        yield return new WaitForSeconds(transitionTime);

        done = true;

        transition.SetTrigger(end);
    }

    public bool IsDone()
    {
        return done;
    }
}
