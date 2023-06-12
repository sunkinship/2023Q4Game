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
    public float bounceStrengh;

    [Header("Dash State")]
    public float dashSpeed;
    public float dashTime;
    public int amountOfDashes;
    public float dashCD;
    public float collisionCheckRadius;

    [Header("Check Variables")]
    public LayerMask ground;
    public LayerMask bounce;
    public LayerMask checkPoint;
    public LayerMask hazard;
    public LayerMask camChange;
    public LayerMask sceneChange;
    public float groundCheckRadius;

    [Header("Audio Clips")]
    public AudioClip walkClip;
    public AudioClip jumpClip;
    public AudioClip doubleJumpClip;
}