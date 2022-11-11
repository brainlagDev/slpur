using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventDelayedListener : GameEventListener
{
    [SerializeField] private float Delay;
    [SerializeField] private UnityEvent DelayedUnityEvent;

    public override void RaiseEvent()
    {
        UnityEvent.Invoke();
        StartCoroutine(RunDelayedEvent());
    }

    private IEnumerator RunDelayedEvent()
    {
        yield return new WaitForSeconds(Delay);
        DelayedUnityEvent.Invoke();
    }
}
