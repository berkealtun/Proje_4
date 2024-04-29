using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Yeni giriş sistemini kullanmak için bu satır eklenmiştir.

public class PlayerMovement : MonoBehaviour
{
    public float pistonJumpForce = 10f;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 2;
    private int jumpsRemaining;
    private bool isGrounded;
    private bool isWallSliding;
    public float dashForce = 30f;
    public float dashDuration = 0.05f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private Rigidbody2D rb;

    private PlayerInputs playerInputs; // Yeni giriş sistemi tanımı.

    private void Awake()
    {
        playerInputs = new PlayerInputs();
    }

    void Start()
    {
        jumpsRemaining = maxJumps;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Yeni giriş sistemini etkinleştir.
        playerInputs.Player.Enable();
    }

    void OnDisable()
    {
        // Yeni giriş sistemini devre dışı bırak.
        playerInputs.Player.Disable();
    }

    void Update()
    {
        // Klavye girişlerini alma yerine yeni giriş sistemini kullan.

        // Yeni giriş sisteminden "Move" eyleminin değerlerini al.
        Vector2 movementInput = playerInputs.Player.Move.ReadValue<Vector2>();

        // Hareket vektörünü oluştur.
        Vector2 movement = new Vector2(movementInput.x * moveSpeed, rb.velocity.y);

        // Nesnenin konumunu güncelle.
        rb.velocity = movement;

        // Space tuşuna basılıp basılmadığını kontrol et.
        if (playerInputs.Player.Jump.triggered)
        {
            if (isGrounded || jumpsRemaining > 0)
            {
                Jump();
            }
            else if (isWallSliding)
            {
                WallJump();
            }
        }

        // Dash input kontrolü
        if (playerInputs.Player.Dash.triggered && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x + (dashForce * Mathf.Sign(rb.velocity.x)), rb.velocity.y);

        while (dashTimer < dashDuration)
        {
            dashTimer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        dashTimer = 0f;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpsRemaining--;

        if (!isGrounded && jumpsRemaining == 0)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = false;
        }
    }

    void WallJump()
    {
        float wallJumpForce = 8f;
        float wallJumpDirection = isWallSliding ? -1f : 1f;
        rb.velocity = new Vector2(wallJumpDirection * jumpForce, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsRemaining = maxJumps;
            isWallSliding = false;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isWallSliding = true;
        }

        if (collision.gameObject.CompareTag("Piston"))
        {
            rb.velocity = new Vector2(rb.velocity.x, pistonJumpForce);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWallSliding = false;
        }
    }
}
