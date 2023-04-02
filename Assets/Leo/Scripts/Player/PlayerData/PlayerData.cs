using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float walkSpeed;

    [Header("Jump State")]
    public float jumpStrength;
    public float maxJumpTime;
    public int amountOfJumps;

    [Header("Dash State")]
    public float dashSpeed;
    public float dashTime;
    public int amountOfDashes;
    public float dashCD;
    public float collisionCheckRadius;

    [Header("Check Variables")]
    public LayerMask ground;
    public float groundCheckRadius;
}
