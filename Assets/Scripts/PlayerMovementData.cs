using System;
using System.Collections;
using moving;
using UnityEngine;

[Serializable]
public class PlayerMovementData
{
    [SerializeField] public float moveSpeed = 6.5F;
    [SerializeField] public float jumpVelocity = 24F;
    [SerializeField] public float dashingTimeSeconds = 0.2F;
    [SerializeField] public float dashingCooldownTime = 0.2F;
    [SerializeField] public float dashingPower = 12F;
    [SerializeField] public float notGroundedMoveMultiplayer = 1F;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;
}