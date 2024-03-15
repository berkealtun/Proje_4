using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float moveSpeed = 3f; // Hareket hızı
    public float jumpForce = 5f; // Zıplama kuvveti
    private bool isGrounded; // Yerde mi kontrolü için flag

    void Update()
    {
        // Klavye girişlerini al
        float horizontalInput = Input.GetAxis("Horizontal");

        // Hareket vektörü oluştur
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

        // Nesnenin konumunu güncelle
        transform.position += movement * moveSpeed * Time.deltaTime;

        // "Space" tuşuna basılıp basılmadığını kontrol et ve yerde mi kontrolü yap
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // Zıplama kuvvetini uygula
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Yere temas ettiğini kontrol et
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Yerden ayrıldığını kontrol et
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
