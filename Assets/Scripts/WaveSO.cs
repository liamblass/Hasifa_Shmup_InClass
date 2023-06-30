using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New wave", menuName = "waves")]

public class WaveSO : ScriptableObject
{
    public GameObject path;

    public float speed;

    public float timeBetweenEnemies;

    public List<EnemySO> enemies = new List<EnemySO>();

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in path.transform)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public List<EnemySO> GetEnemies()
    {
        return enemies;
    }
}
