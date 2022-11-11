using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

public class Inventory : MonoBehaviour
{
    #region VARIABLES

    private Player Player;

    public List<Item> Items;
    public List<InventoryItem> InventoryItems;

    #endregion  // VARIABLES

    #region MONODEVELOP_CONSTRUCTIONS

    private void Start()
    {
        Player = GetComponent<Player>();
    }

    private void Update()
    {
        
    }
    
    #endregion

    public void UseItem()
    {
        
    }
    
    public void UpdateItems()
    {
        
    }
}
