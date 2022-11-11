using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] protected GameEvent GameEvent;
    [SerializeField] protected UnityEvent UnityEvent;

    private void Awake()
    {
        GameEvent.Register(this);
    }

    private void OnDestroy()
    {
        GameEvent.Deregister(this);
    }

    public virtual void RaiseEvent() => UnityEvent.Invoke();
}
