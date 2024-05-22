using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Input kontrolü
        float moveX = Input.GetAxis("Horizontal");
        movement = new Vector2(moveX, 0f);

        // Sprite'ı çevirme
        if (moveX > 0)
        {
            spriteRenderer.flipX = true; // Sağa bakıyor
        }
        else if (moveX < 0)
        {
            spriteRenderer.flipX = false; // Sola bakıyor
        }
    }

    void FixedUpdate()
    {
        // Yatay hareket
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }
}
