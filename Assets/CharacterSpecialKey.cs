using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;

public enum DashInvin
{
    None,
    Start,
    End
}

[RequireComponent(typeof(PlayerCharacter))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterSpecialKey : MonoBehaviour
{
    private PlayerCharacter playerCharacter;
    private CharacterController2D characterController2D;
    private SpriteRenderer spriteRenderer;
    private Silhouette silhouette;

    public KeyCode dashKey = KeyCode.LeftShift;
    public KeyCode gliderKey = KeyCode.Space;

    [Header("기술 사용 설정")]
    public bool onDash = false;
    public bool onGlider = false;
    public bool onDoubleJump = false;

    [Header("대쉬 설정")]
    public float dashTime = 0.2f;
    public float dashSpeed = 10f;
    public bool isDashing = false;
    public DashInvin dash_Invin = DashInvin.None;
    public bool dashCooldown = false;
    [SerializeField]
    private float runTimeDash;

    [Header("글라이딩 중력 설정")]
    public float glideFallSpeed = 25;
    public float originalGravity = 50f;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        silhouette = GetComponent<Silhouette>();
        playerCharacter = GetComponent<PlayerCharacter>();
        characterController2D = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (onDash)
        {
            if (Input.GetKeyDown(dashKey) && !dashCooldown)
            {
                isDashing = true;
            }
            if (characterController2D.IsGrounded)
            {
                dashCooldown = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing)
        {
            gameObject.GetComponent<Damageable>().EnableInvulnerability(true);
        }
        else if(!isDashing && dash_Invin == DashInvin.End)
        {
            gameObject.GetComponent<Damageable>().DisableInvulnerability();
            dash_Invin = DashInvin.None;
        }

        //대쉬
        if (onDash)
        {
            if (!playerCharacter.onPlayerStop)
            {
                if (isDashing)
                {
                    Dash();
                }
            }
        }

        //글라이딩
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

    public void Dash()
    {
        isDashing = true;
        silhouette.Active = true;
        playerCharacter.onMoveStop = true;
        dash_Invin = DashInvin.Start;

        if (runTimeDash < dashTime)
        {
            runTimeDash += Time.deltaTime;
            playerCharacter.m_CharacterController2D.Move(new Vector2(dashSpeed * (spriteRenderer.flipX ? -1 : 1), 0) * Time.deltaTime);
        }
        else
        {
            runTimeDash = 0;
            dashCooldown = true;
            isDashing = false;
            silhouette.Active = false;
            playerCharacter.onMoveStop = false;
            dash_Invin = DashInvin.End;
            playerCharacter.m_CharacterController2D.Move(Vector2.zero);
        }
    }
}
