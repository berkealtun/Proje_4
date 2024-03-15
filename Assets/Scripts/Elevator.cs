using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    

public class Elevator : MonoBehaviour
{
    
public float moveSpeed = 5f; // Platformun ilerleme hızı
    public float moveDistance = 5f; // İlerlenecek maksimum mesafe
    private Vector3 initialPosition; // Platformun başlangıç pozisyonu
    private bool playerOnPlatform = false; // Karakter platform üzerinde mi kontrolü
    private Vector3 playerPlatformOffset; // Karakterin platform üzerindeki konum farkı

    void Start()
    {
        initialPosition = transform.position; // Platformun başlangıç pozisyonu kaydet
    }

    void Update()
    {
        if (playerOnPlatform)
        {
            // Platformu ileri doğru hareket ettirme
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            // Karakteri platformun hareketine bağlı olarak güncelle
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Eğer oyuncu platforma çarparsa
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            // Karakterin platform üzerindeki konum farkını kaydet
            playerPlatformOffset = collision.transform.position - transform.position;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Eğer oyuncu platformdan ayrılırsa
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }

    // Platformun +x yönünde ilerleyip dönmemesi için kendi yerine geri ışınlanmasını engellemek
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - initialPosition.x) >= moveDistance)
        {
            // Platformun başlangıç pozisyonunda kalmasını sağla
            transform.position = new Vector3(initialPosition.x + Mathf.Sign(moveSpeed) * moveDistance, transform.position.y, transform.position.z);
        }
    }
}

    

