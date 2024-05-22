using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Takip edilecek hedef (karakter)
    public Transform target;
    
    // Kameranın karakterden olan mesafesi
    public Vector3 offset;
    
    // Kameranın hareket hızını belirleyen parametre
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // Hedef pozisyonunu hesapla
        Vector3 desiredPosition = target.position + offset;
        
        // Kameranın pozisyonunu yumuşak bir geçişle hedef pozisyonuna taşı
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Kamerayı yeni pozisyona ayarla
        transform.position = smoothedPosition;
    }
}
