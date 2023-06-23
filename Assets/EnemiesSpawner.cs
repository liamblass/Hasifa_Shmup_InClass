using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private List<WaveSO> waves = new List<WaveSO>();
    [SerializeField] private float timeBetweenWaves = 0.5f;
    private WaveSO currentWave;
    [SerializeField] private GameObject enemyPrefab;

    private bool isActive = true;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (isActive)
        {
            foreach (WaveSO wave in waves)
            {
                currentWave = wave;

                List<EnemySO> enemiesParameters = currentWave.GetEnemies();

                for (int i = 0; i < currentWave.GetEnemies().Count; i++)
                {
                    GameObject newEnemy = Instantiate(
                            enemyPrefab,
                            currentWave.GetWaypoints()[0].position,
                            Quaternion.identity,
                            transform);

                    newEnemy.GetComponentInChildren<SpriteRenderer>().sprite = enemiesParameters[i].sprite;
                    newEnemy.GetComponent<Health>().SetInitialHealth(enemiesParameters[i].health);

                    yield return new WaitForSeconds(currentWave.timeBetweenEnemies);

                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }

        }


    }

    public WaveSO GetCurrentWave()
    {
        return currentWave;
    }
}