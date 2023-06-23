using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private List<Transform> waypoints;
    private int currentWaypointIndex = 0;

    public void SetWaypoints(List<Transform> w)
    {
        waypoints = w;
        transform.position = waypoints[0].position;
    }

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {

    }


}
