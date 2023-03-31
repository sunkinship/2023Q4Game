using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour {

    private float lengthX, lenghtY, startPosX, startPosY;
    public GameObject cam;
    public float parallaxAmount;


    private void Start() 
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lengthX = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        lenghtY = GetComponentInChildren<SpriteRenderer>().bounds.size.y;
    }

    private void LateUpdate() 
    {
        float temp = (cam.transform.position.x * (1 - parallaxAmount));
        float distX = (cam.transform.position.x * parallaxAmount);
        float distY = (cam.transform.position.y * parallaxAmount);

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        if (temp > startPosX + (lengthX * 2)) 
            startPosX += lengthX;
        else if (temp < startPosX - (lengthX * 2)) 
            startPosX -= lengthX;
    }
}
