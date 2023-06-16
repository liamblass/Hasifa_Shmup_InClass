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
}
