using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public bool IsActive 
    {
        get
        {
            return gameObject.activeSelf;
        }
    }
    [HideInInspector] public Pool Parent { get; set; }

    public void Release()
    {
        Parent.ReturnToPool(this);
    }
}