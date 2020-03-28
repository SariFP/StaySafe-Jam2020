using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public HouseObject houseObject;
    public Canvas Endscreen;

    public Image[] Stars = new Image[4];

    public TMP_Text FinalText;

    private int aliveCount = 5;

    private void Start()
    {
        CheckForAlive();
        ChangeFinalText();
    }

    public void CheckForAlive()
    {
        for (int i = 0; i < houseObject.Ressource.Length; i++)
        {
            if (houseObject.Ressource[i].Dead)
            {
                Stars[i].gameObject.SetActive(false);
                aliveCount--;
            }
        }

        if (houseObject.Ressource[0].Dead)
        {
            Stars[0].gameObject.SetActive(false);
            Stars[1].gameObject.SetActive(false);
            Stars[2].gameObject.SetActive(false);
            Stars[3].gameObject.SetActive(false);
            Stars[4].gameObject.SetActive(false);

            aliveCount = 0;
        }
    }

    public void ChangeFinalText()
    {
        if (aliveCount == 5)
        {
            FinalText.text = " Save ";
        }
        else if (aliveCount == 4)
        {
            FinalText.text = " Good ";
        }
        else if (aliveCount == 3)
        {
            FinalText.text = " Well... ";
        }
        else if (aliveCount == 2)
        {
            FinalText.text = " Wait... ";
        }
        else if (aliveCount == 1)
        {
            FinalText.text = " Wtf happened?! ";
        }
        else
        {
            FinalText.text = " What?? ";
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
