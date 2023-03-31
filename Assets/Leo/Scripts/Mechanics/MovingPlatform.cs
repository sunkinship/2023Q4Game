using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed;
    public Transform[] wayPoints;
    public float checkDistance = 0.05f;

    private Transform targetWayPoint;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        targetWayPoint = wayPoints[0];
    }

    private void FixedUpdate()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
