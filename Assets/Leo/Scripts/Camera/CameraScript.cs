using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("General Camera Settings")]
    [SerializeField] private Transform target;
    public Vector2 minPos, maxPos;
    public Vector3 offset;
    [SerializeField] private float followSmoothing;
    private bool followTarget = true;

    [Header("Camera Shake Settings")]
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float shakeDuration = 1f;


    private void FixedUpdate()
    {
        FollowTarget();
    }

    #region Camera Follow
    private void FollowTarget()
    {
        if (followTarget && transform.position != target.position)
        {
            transform.position = Vector3.Lerp(transform.position, GetFinalPos() + offset, followSmoothing);
        }
    }

    private Vector3 GetFinalPos()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z)
        {
            x = Mathf.Clamp(target.position.x, minPos.x, maxPos.x),
            y = Mathf.Clamp(target.position.y, minPos.y, maxPos.y)
        };
        return targetPosition;
    }

    public void DisableCameraFollow() => followTarget = false;

    public void EnableCameraFollow()
    {
        transform.position = GetFinalPos() + offset;
        followTarget = true;
    }
    #endregion

    #region Camera Shake
    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        Vector3 startPosition = transform.position;
        float elaspedTime = 0f;

        while (elaspedTime < shakeDuration)
        {
            elaspedTime += Time.deltaTime;
            float strength = curve.Evaluate(elaspedTime / shakeDuration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;  
        }

        transform.position = startPosition;
    }
    #endregion
}