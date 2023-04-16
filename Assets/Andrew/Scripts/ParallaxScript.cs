using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour {

    private float lengthX, startPosX, startPosY;
    public GameObject cam;
    public float xParallaxAmount, yParallaxAmount;
    [SerializeField]
    private bool repositionSooner;
    [SerializeField]
    private Vector2 offset;


    private void Start() 
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lengthX = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    private void LateUpdate() 
    {
        float temp = (cam.transform.position.x * (1 - xParallaxAmount));
        float distX = (cam.transform.position.x * xParallaxAmount);
        float distY = (cam.transform.position.y * yParallaxAmount);

        transform.position = new Vector3(startPosX + distX + offset.x, startPosY + distY + offset.y, transform.position.z);

        Reposition(temp);
    }

    private void Reposition(float temp)
    {
        if (repositionSooner == false)
        {
            if (temp > startPosX + (lengthX * 2))
                startPosX += lengthX;
            else if (temp < startPosX - (lengthX * 2))
                startPosX -= lengthX;
        }
        else
        {
            if (temp > startPosX + lengthX)
                startPosX += lengthX;
            else if (temp < startPosX - lengthX)
                startPosX -= lengthX;
        }
    }
}
