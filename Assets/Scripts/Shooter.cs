using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Shooter: MonoBehaviour
{
    public UnityEvent OnShoot;
    
    [Header("GENERAL")]
    //[SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifeTime;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private PoolNames projectilesPoolName;

    [Header("FOR ENEMIES ONLY")]
    [SerializeField] private bool isEnemy = false;
    [HideInInspector] public bool isShooting;
    [SerializeField] private float fireRateVariance = 0.2f;
    [SerializeField] private float fireRateMinimum = 0.5f;

    // please add a time variance to the enemies shooting rate

    Coroutine fireCoroutine;

    private void Start()
    {
        if (isEnemy)
            isShooting = true;
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        // THIS IS WRONG
        /* 
        if (isShooting)
        {
            fireCoroutine = StartCoroutine(ContinousFireCoroutine());
        }
        else // isShooting = false
        {
            StopCoroutine(fireCoroutine);
        }
        */

        if (isShooting && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(ContinousFireCoroutine());
        }
        else if (!isShooting && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    private IEnumerator ContinousFireCoroutine()
    {
        while (true)
        {
            /*GameObject newProjectile = Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity);*/

            GameObject newProjectile = PoolManager.Instance.TakeFromPool(
                projectilesPoolName,
                transform.position,
                Quaternion.identity);

            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.up * projectileSpeed;

            OnShoot?.Invoke();

            //Destroy(newProjectile, projectileLifeTime);
            PoolManager.Instance.ReturnToPool(newProjectile, projectileLifeTime);

            //////////
            float timeToWait = UnityEngine.Random.Range(
                timeBetweenShots - fireRateVariance,
                timeBetweenShots + fireRateVariance);

            timeToWait = Mathf.Max(timeToWait, fireRateMinimum);

            yield return new WaitForSeconds(timeToWait);
        }
    }
}


