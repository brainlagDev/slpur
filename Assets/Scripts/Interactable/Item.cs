using System;
using System.Collections;
using UnityEngine;

public class Item 
{
    #region VARIABLES

    public ItemType Type;
    public string Name;
    public string Description;
    public SpriteRenderer Sprite;
    [SerializeField] private ItemInstance InstancePrefab;

    #endregion // VARIABLES

    /// <summary>
    /// Overridable method
    /// </summary>
    public virtual void Use()
    {

    }
}

public enum ItemType
{

}