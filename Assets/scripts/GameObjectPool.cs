using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public GameObject[] objectsToPool;
    public int poolSize;

    private List<GameObject>[] objectPools;

    void Start()
    {
        objectPools = new List<GameObject>[objectsToPool.Length];

        for (int i = 0; i < objectsToPool.Length; i++)
        {
            objectPools[i] = new List<GameObject>();
            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(objectsToPool[i]);
                obj.SetActive(false);
                objectPools[i].Add(obj);
            }
        }

       // Debug.Log("Object pool created with size: " + (poolSize * objectsToPool.Length));
       // Debug.Log("Current object pool size: " + GetObjectPoolSize());
    }

    public int GetObjectPoolSize()
    {
        int size = 0;

        for (int i = 0; i < objectPools.Length; i++)
        {
            size += objectPools[i].Count;
        }

        return size;
    }

    public GameObject GetObject(int objectIndex)
    {
        if (objectIndex < 0 || objectIndex >= objectsToPool.Length)
        {
            Debug.LogError("Invalid object index!");
            return null;
        }

        List<GameObject> pool = objectPools[objectIndex];

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        if (pool.Count < poolSize)
        {
            GameObject obj = Instantiate(objectsToPool[objectIndex]);
            obj.SetActive(false);
            pool.Add(obj);
            return obj;
        }

        Debug.LogWarning("Object pool exhausted for object index " + objectIndex);
        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = new Vector3(0, 0, 0);
    }
}