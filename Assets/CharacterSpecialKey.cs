using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;

[RequireComponent(typeof(PlayerCharacter))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterSpecialKey : MonoBehaviour
{
    private PlayerCharacter playerCharacter;
    private CharacterController2D characterController2D;

    public KeyCode dashKey = KeyCode.LeftShift;
    public KeyCode gliderKey = KeyCode.Space;

    [Header("기술 사용 설정")]
    public bool onDash = false;
    public bool onGlider = false;
    public bool onDoubleJump = false;

    [Header("대쉬 설정")]
    public float dashMaxSpeed = 14;
    public float orginSpeed = 7;

    [Header("글라이딩 중력 설정")]
    public float glideFallSpeed = 25;
    public float originalGravity = 50f;

    // Start is called before the first frame update
    void Awake()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        characterController2D = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onDash)
        {
            if (Input.GetKey(dashKey))
            {
                playerCharacter.maxSpeed = dashMaxSpeed;
            }
            else if (Input.GetKeyUp(dashKey))
            {
                playerCharacter.maxSpeed = orginSpeed;
            }
        }

        if (onGlider)
        {
            if (!characterController2D.IsGrounded && Input.GetKey(gliderKey))
            {
                if (playerCharacter.GetMoveVector().y < -glideFallSpeed)
                {
                    playerCharacter.SetVerticalMovement(-glideFallSpeed);
                }
            }
            else
            {
                playerCharacter.gravity = originalGravity;
            }
        }
    }
}
