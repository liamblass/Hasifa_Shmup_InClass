using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    public int GetDamage()
    {
        return damage;
    }

    public void HitSomething()
    {
        //Destroy(gameObject);
        PoolManager.Instance.ReturnToPool(gameObject);
    }
}
