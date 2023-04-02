using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ControlBlur : MonoBehaviour
{
    private Volume volume;
    private DepthOfField depth;


    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out depth);
        DisableBlur();
    }

    public void EnableBlur()
    {
       depth.active = true;
       depth.focusDistance.value = 1;
    }

    public void DisableBlur()
    {
        depth.focusDistance.value = 100;
        depth.active = false;
    }
}
