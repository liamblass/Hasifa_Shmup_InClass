using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IPoolable
{
    private ParticleSystem ps;

    public void OnReturnToPool()
    {
        
    }

    public void OnTakeFromPool()
    {
        Explode();
        PoolManager.Instance.ReturnToPool(gameObject, ps.main.startLifetime.constant * 1.5f);
    }

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Explode()
    {
        ps.Play();
    }

    
}
