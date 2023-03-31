using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float jumpPower;
    public float speed;

    [Header("Jump State")]
    public float maxJumpTime;
    public int amountOfJumps;

    [Header("Check Variables")]
    public LayerMask ground;
    public float groundCheckRadius;
}
