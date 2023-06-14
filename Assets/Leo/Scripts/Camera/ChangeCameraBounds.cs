using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraBounds : MonoBehaviour
{
    private CameraScript cam;

    [SerializeField] protected Vector2 newMin, newMax;

    private void Start()
    {
        cam = Camera.main.GetComponent<CameraScript>();
    }

    public void UpdateCamBounds()
    {
        cam.minPos = newMin;
        cam.maxPos = newMax;
    }
}
