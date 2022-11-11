using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game Event", fileName ="New Game Event")]
public class GameEvent : ScriptableObject
{
    HashSet<GameEventListener> Listeners = new HashSet<GameEventListener>();

    public void Invoke()
    {
        foreach (var item in Listeners)
        {
            item.RaiseEvent();
        }
    }

    public void Register(GameEventListener listener) => Listeners.Add(listener);

    public void Deregister(GameEventListener listener) => Listeners.Remove(listener);
}