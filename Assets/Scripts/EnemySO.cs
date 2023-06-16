using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy", menuName = "enemies")]
public class EnemySO : ScriptableObject
{
    public int health;
    public Sprite sprite;
}
