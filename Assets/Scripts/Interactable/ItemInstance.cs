using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInstance : InteractableObject
{
    public Item Item;
    public override void Interact()
    {
        base.Interact();

        GameManager.PlayerAddItem(Item);
        Destroy(this.gameObject);
    }
}
