using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameOptions", menuName ="ScriptableObjects/GameOptions", order =1)]
public class GameOptions : ScriptableObject
{
    #region Player parameters
    [Header("Player parameters")]

    public float PlayerMoveSpeed;

    public float PlayerJumpForce;



    #endregion
}
