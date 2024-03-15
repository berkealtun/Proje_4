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

    void Start()
    {
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        // Klavye girişlerini al
        float horizontalInput = Input.GetAxis("Horizontal");

        // Hareket vektörü oluştur
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

        // Nesnenin konumunu güncelle
        transform.position += movement * moveSpeed * Time.deltaTime;

        // "Space" tuşuna basılıp basılmadığını kontrol et ve yerde veya double jump hakkı var mı kontrol et
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpsRemaining > 0))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Zıplama kuvvetini uygula
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce);
        jumpsRemaining--;

        // Eğer hala double jump hakkı varsa, hakkı güncelle
        if (jumpsRemaining == 0)
        {
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Yere temas ettiğini kontrol et
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }
    }
}
