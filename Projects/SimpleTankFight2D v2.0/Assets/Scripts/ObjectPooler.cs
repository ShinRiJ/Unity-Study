using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public String tag;
        public GameObject prefab;
        public Int32 size;
    }

    public static ObjectPooler Instance;


    public List<Pool> pools;
    public Dictionary<String, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ReInit();
        }
    }

    private void Start()
    {
        poolDictionary = new Dictionary<String, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(Int32 index = 0; index < pool.size; index++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public void ReInit()
    {
        poolDictionary = new Dictionary<String, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (Int32 index = 0; index < pool.size; index++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(String tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject objToSpawn = poolDictionary[tag].Dequeue();

        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        objToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
}
