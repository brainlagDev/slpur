using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Input Settings", menuName = "Player/Player Input Settings")]
public class PlayerInputSettings : ScriptableObject
{
    #region General
    [Header("General")]

    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode MoveUp;
    public KeyCode MoveDown;
    public KeyCode Jump;
    public KeyCode Dash;
    public KeyCode Duck;

    [Space]
    #endregion

    #region Combat
    [Header("Combat")]

    public KeyCode LightAttack;
    public KeyCode HeavyAttack;
    public KeyCode Block;

    [Space]
    #endregion

    #region Other
    [Header("Other")]

    public KeyCode Heal;
    public KeyCode Interact;
    public KeyCode Menu;

    //[Space]
    #endregion

    public int Horizontal()
    {
        int axis = 0;
        if (Input.GetKey(MoveLeft))
        {
            axis -= 1;
        }
        if (Input.GetKey(MoveRight))
        {
            axis += 1;
        }
        return axis;
    }

    public int Vertical()
    {
        int axis = 0;
        if (Input.GetKey(MoveDown))
        {
            axis -= 1;
        }
        if (Input.GetKey(MoveUp))
        {
            axis += 1;
        }
        return axis;
    }
}
