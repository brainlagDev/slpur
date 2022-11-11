using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public string InteractionText;
    public List<UnityEvent> InteractEvents;

    /// <summary>
    /// Overridable method for object function. Call base.Interact() for invoking all events
    /// </summary>
    public virtual void Interact() 
    {
        foreach(var item in InteractEvents)
        {
            item.Invoke();
        }
    }
}
