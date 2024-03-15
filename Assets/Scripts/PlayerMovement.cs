using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
 public float moveSpeed = 5f; // Hareket hızı
    public float jumpForce = 10f; // Zıplama kuvveti
    public int maxJumps = 2; // Maksimum zıplama sayısı
    private int jumpsRemaining; // Kalan zıplama sayısı
    private bool isGrounded; // Yerde mi kontrolü için flag
    private bool isWallSliding; // Duvara kayma kontrolü için flag
    private Rigidbody2D rb; // Rigidbody bileşeni referansı

    void Start()
    {
        jumpsRemaining = maxJumps;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Klavye girişlerini al
        float horizontalInput = Input.GetAxis("Horizontal");

        // Hareket vektörü oluştur
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Nesnenin konumunu güncelle
        rb.velocity = movement;

        // "Space" tuşuna basılıp basılmadığını kontrol et
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || jumpsRemaining > 0)
            {
                Jump();
            }
            else if (isWallSliding) // Duvara yapışıyorsak ve Space'e basarsak
            {
                WallJump();
            }
        }
    }

   void Jump()
{
    // Zıplama kuvvetini uygula
    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    jumpsRemaining--;

    // Eğer hala double jump hakkı varsa, hakkı güncelle
    if (!isGrounded && jumpsRemaining == 0)
    {
        isGrounded = false;
    }
    else
    {
        isGrounded = false; // Bu kısmı ekle
    }
}

    void WallJump()
    {
        // Duvara yapışma durumundayken zıplama kuvvetini uygula
        float wallJumpForce = 8f; // Duvar zıplama kuvveti
        float wallJumpDirection = isWallSliding ? -1f : 1f; // Duvara göre zıplama yönü
        rb.velocity = new Vector2(wallJumpDirection * jumpForce, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Yere veya duvara temas ettiğini kontrol et
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
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Duvardan ayrıldığını kontrol et
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWallSliding = false;
        }
    }
}
