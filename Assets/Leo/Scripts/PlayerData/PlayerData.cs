using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float jumpPower;
    public float speed;

    [Header("Jump")]
    public LayerMask ground;
    public float groundCheckRadius;
    public float maxJumpTime;
}
