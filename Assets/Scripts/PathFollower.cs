using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private List<Transform> waypoints;
    private int currentWaypointIndex = 0;
    private float moveSpeed;

    public void SetFollowParams(List<Transform> points, float speed)
    {
        waypoints = points;
        transform.position = waypoints[0].position;

        moveSpeed = speed;
    }


    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            Vector3 destination = waypoints[currentWaypointIndex].position;

            transform.position = Vector2.MoveTowards(
                transform.position,
                destination,
                moveSpeed * Time.deltaTime);

            if (transform.position == destination)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
