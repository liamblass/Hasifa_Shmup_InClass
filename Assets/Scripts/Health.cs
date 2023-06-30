using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event Action OnEnemyDead;
    public static event Action OnPlayerDead;

    public static event Action<int, int> OnPlayerLoseHealth;

    [SerializeField] private int initialHealth = 5;
    private int currentHealth;

    [SerializeField] private bool isPlayer = false;

    private CameraShake cameraShake;

    private void Awake()
    {
        if(isPlayer)
        {
            ResetHealth();
        }
    }

    private void Start()
    {
        if (isPlayer)
            cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void LoseHealth(int loseAmount)
    {
        currentHealth -= loseAmount;

        if(isPlayer)
        {
            OnPlayerLoseHealth?.Invoke(initialHealth, currentHealth);
        }

        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            if (isPlayer)
            {
                OnPlayerDead?.Invoke();
            }
            else
            {
              OnEnemyDead?.Invoke();
          
            }

            PoolManager.Instance.TakeFromPool(
                PoolNames.explosion,
                transform.position,
                Quaternion.identity);
            
            Destroy(gameObject);
        }
    }

    private void ResetHealth()
    {
        currentHealth = initialHealth;
    }

    public void SetInitialHealth(int h)
    {
        currentHealth = h;
        initialHealth = h;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Damager damager = collision.GetComponent<Damager>();

        if (damager != null)
        {

        }*/

        if (collision.TryGetComponent<Damager>(out Damager damager))
        {
            LoseHealth(damager.GetDamage());
            damager.HitSomething();

            if (isPlayer && cameraShake != null)
            {
                cameraShake.Shake();
            }
        }
    }

}
