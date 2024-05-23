using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicDoor : MonoBehaviour
{
    public MagicPiece magicPiece;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (magicPiece != null && magicPiece.IsCollected())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load next level
            }
            else
            {
                Debug.Log("You need to collect the magic piece first!");
            }
        }
    }
}
