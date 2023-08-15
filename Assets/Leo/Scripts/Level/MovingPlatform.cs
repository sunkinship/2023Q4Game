using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed;
    public Transform[] wayPoints;
    public float checkDistance = 0.05f;
    public bool moveWhenTouched;

    private bool activated;
    private Transform targetWayPoint;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        targetWayPoint = wayPoints[0];
        if (moveWhenTouched == false)
            activated = true;
    }

    private void FixedUpdate()
    {
        if (activated)
            transform.position = Vector2.MoveTowards(transform.position, targetWayPoint.position, moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, targetWayPoint.position) < checkDistance)
        {
            targetWayPoint = GetNextWayPoint();
        }
    }

    private Transform GetNextWayPoint()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex >= wayPoints.Length)
        {
            currentWaypointIndex = 0;
        }

        return wayPoints[currentWaypointIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FeetPos"))
        {
            activated = true;
            collision.transform.parent.gameObject.transform.parent.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FeetPos"))
        {
            collision.transform.parent.gameObject.transform.parent.gameObject.transform.SetParent(null);
        }
    }
}
