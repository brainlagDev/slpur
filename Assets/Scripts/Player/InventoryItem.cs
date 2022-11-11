using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public Item Item;

    void OnMouseDown()
    {
        Item.Use();
    }
}
