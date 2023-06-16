using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //[SerializeField] private Pool playerProjectilesPool;
    //[SerializeField] private Pool enemyProjectilesPool;
    [SerializeField] private List<Pool> pools = new List<Pool>();

    //private Queue<GameObject> playerProjectilesQueue = new Queue<GameObject>();
    //private Queue<GameObject> enemyProjectilesQueue = new Queue<GameObject>();
    private Dictionary<PoolNames, Queue<GameObject>> poolsDictionary = new Dictionary<PoolNames, Queue<GameObject>>();

    public static PoolManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InitializePool();
    }

    /*private void InitializePool()
    {
        for (int i=0; i< playerProjectilesPool.size; i++)
        {
            GameObject obj = Instantiate(playerProjectilesPool.prefab, transform);
            obj.SetActive(false);

            playerProjectilesQueue.Enqueue(obj);
        }

        for (int i = 0; i < enemyProjectilesPool.size; i++)
        {
            GameObject obj = Instantiate(enemyProjectilesPool.prefab, transform);
            obj.SetActive(false);

            enemyProjectilesQueue.Enqueue(obj);
        }
    }*/

    private void InitializePool()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectsQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);

                objectsQueue.Enqueue(obj);
            }

            poolsDictionary.Add(pool.name, objectsQueue);
        }
    }


    public GameObject TakeFromPool(PoolNames poolName, Vector3 position, Quaternion rotation)
    {
        if (!poolsDictionary.ContainsKey(poolName))
        {
            Debug.Log("the pools dictionary does not contain a key called " + poolName);
            
            return null;
        }

        GameObject objectToSpawn = poolsDictionary[poolName].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolsDictionary[poolName].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ReturnToPool(GameObject obj, float timeDelay)
    {
        StartCoroutine(DespawnCoroutine(obj, timeDelay));
    }

    private IEnumerator DespawnCoroutine(GameObject obj, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        ReturnToPool(obj);
    }
}

[Serializable]
public class Pool
{
public PoolNames name;
public int size;
public GameObject prefab;
}

public enum PoolNames
{
    playerProjectiles,
    enemyProjectiles
}


