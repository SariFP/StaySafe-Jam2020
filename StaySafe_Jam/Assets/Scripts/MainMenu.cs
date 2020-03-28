using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas MainCanvas;
    public Canvas CreditCanvas;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void CreditScreen(bool change)
    {
        if (change)
        {
            MainCanvas.gameObject.SetActive(false);
            CreditCanvas.gameObject.SetActive(true);
        }
        else
        {
            MainCanvas.gameObject.SetActive(true);
            CreditCanvas.gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}
