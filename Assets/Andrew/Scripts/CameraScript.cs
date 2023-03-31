using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Vector2 minPos, maxPos;
    public Vector3 offset;
    [SerializeField] private float followSmoothing;

    private void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z)
            {
                x = Mathf.Clamp(target.position.x, minPos.x, maxPos.x),
                y = Mathf.Clamp(target.position.y, minPos.y, maxPos.y)
            };

            transform.position = Vector3.Lerp(transform.position, targetPosition + offset, followSmoothing);
        }
    }
}

