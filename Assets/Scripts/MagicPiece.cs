using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicPiece : MonoBehaviour
{
     private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCollected = true;
            gameObject.SetActive(false); // Büyü parçacığını görünmez yap
        }
    }

    public bool IsCollected()
    {
        return isCollected;
    }
}
