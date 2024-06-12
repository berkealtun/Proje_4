using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Burada "SampleScene" yerine oyun sahnenizin ismini yazın.
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("Oyundan çıkış yapıldı!");
        Application.Quit();
    }
}