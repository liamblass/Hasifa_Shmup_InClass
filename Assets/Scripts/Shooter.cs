using System;
using System.Collections;
using UnityEngine;

public class Shooter: MonoBehaviour
{
    [Header("GENERAL")]
    //[SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifeTime;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private PoolNames projectilesPoolName;

    //[Header("FOR ENEMIES ONLY")]
    [SerializeField] private bool isEnemy = false;
    [HideInInspector] public bool isShooting;

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

            //Destroy(newProjectile, projectileLifeTime);
            PoolManager.Instance.ReturnToPool(newProjectile, projectileLifeTime);

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}


