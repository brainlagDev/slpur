using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public int Size
    {
        get { return PoolObjects.Count; }
    }
    public int AvialableObjectCount
    {
        get
        {
            int counter = 1;
            for (int i = 0; i < PoolObjects.Count; i++)
            {
                if (PoolObjects[i].gameObject.activeSelf)
                    counter++;
            }
            return counter;
        }
    }

    private Transform PoolContainer;

    private List<PoolObject> PoolObjects = new List<PoolObject>();
    private PoolObject PoolPrefab;

    private int DefaultCount;
    private int MaxCount;

    public Pool(PoolObject poolPrefab, int defaultCount, int maxCount, string poolStorageName = "PoolStorage")
    {
        PoolContainer = new GameObject(poolStorageName).transform;

        PoolPrefab = poolPrefab;
        DefaultCount = defaultCount;
        MaxCount = maxCount;

        for (int i = 0; i < DefaultCount; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        GameObject newObject = UnityEngine.Object.Instantiate(PoolPrefab, PoolContainer).gameObject;
        newObject.transform.position = Vector3.zero;
        newObject.SetActive(false);
        newObject.GetComponent<PoolObject>().Parent = this;
        PoolObjects.Add(newObject.GetComponent<PoolObject>());
    }

    public void ReturnToPool(PoolObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
    }

    public GameObject Get(Transform spawn = null)
    {
        for (int i = 0; i < PoolObjects.Count; i++)
        {
            if (!PoolObjects[i].gameObject.activeSelf)
            {
                PoolObjects[i].gameObject.SetActive(true);
                if (spawn != null)
                    PoolObjects[i].transform.position = spawn.position;
                return PoolObjects[i].gameObject;
            }
        }

        if (PoolObjects.Count < MaxCount)
        {
            CreateObject();
            PoolObjects[PoolObjects.Count - 1].gameObject.SetActive(true);
            if (spawn != null)
                PoolObjects[PoolObjects.Count - 1].transform.position = spawn.position;
            return PoolObjects[PoolObjects.Count - 1].gameObject;
        }

        return null;
    }

    public void Clear()
    {
        foreach (var item in PoolObjects)
        {
            UnityEngine.Object.Destroy(item.gameObject);
        }
        PoolObjects.Clear();
    }
}