using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Settings", menuName = "Player/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Base settings")]
    public float MoveSpeed;
    public float DuckSpeed;
    public float JumpForce;
    public float LandTime;
    [Space]

    [Header("Healing")]
    public int StartHealingPotions;
    public int MaxHealingPotions;
    public float PotionRegen;
    [Space]

    [Header("Dash")]
    public float DashDistance;
    public float DashCooldown;
    [Space]

    [Header("Climb")]
    public float ClimbSpeed;
    public float ClimbRadius;
}
