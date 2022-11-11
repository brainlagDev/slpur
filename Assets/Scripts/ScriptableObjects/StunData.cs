using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stun Data", menuName = "Combat/Stun Data")]
public class StunData : ScriptableObject
{
    public int Chance;
    public float StunTime;
    public float DiscardForce;
}