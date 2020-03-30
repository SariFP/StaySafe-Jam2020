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

    public AudioClip ClickSound;

    public Image[] Stars = new Image[4];

    public TMP_Text TitleText;
    public TMP_Text FinalText;

    private int aliveCount = 5;

    private void Start()
    {
        CheckForAlive();

        if (aliveCount > 0)
            ChangeFinalText();

        else if (aliveCount == 0)
            GameOver();

    }

    public void CheckForAlive()
    {
        for (int i = 0; i < houseObject.Ressource.Length; i++)
        {
            if (houseObject.Ressource[i].Dead)
            {
                aliveCount--;
            }
        }

        if (houseObject.Ressource[0].Dead)
        {
            aliveCount = 0;
        }
    }

    public void GameOver()
    {
        TitleText.text = "Game Over";
        FinalText.text = " You alone managed to persevere. The cows outside are the only company you have left.";
        Stars[0].gameObject.SetActive(false);
        Stars[1].gameObject.SetActive(false);
        Stars[2].gameObject.SetActive(false);
    }

    public void ChangeFinalText()
    {
        if (aliveCount == 5)
        {
            FinalText.text = " Congratulations! You managed to keep both yourself and all of your neighbours sufficiently sustained during quarantine. Well done! ";
            Stars[0].gameObject.SetActive(true);
            Stars[1].gameObject.SetActive(true);
            Stars[2].gameObject.SetActive(true);
        }
        else if (aliveCount >= 3)
        {
            FinalText.text = " Congratulations, you persevered and ensured another family could push through as well! ";
            Stars[0].gameObject.SetActive(true);
            Stars[1].gameObject.SetActive(false);
            Stars[2].gameObject.SetActive(true);
        }
        else if (aliveCount >= 2)
        {
            FinalText.text = " Congratulations, you persevered and ensured another family could push through as well! ";
            Stars[0].gameObject.SetActive(true);
            Stars[1].gameObject.SetActive(false);
            Stars[2].gameObject.SetActive(false);

        }
        else if (aliveCount <= 1)
        {
            FinalText.text = " You alone managed to persevere. The cows outside are the only company you have left.";
            Stars[0].gameObject.SetActive(false);
            Stars[1].gameObject.SetActive(false);
            Stars[2].gameObject.SetActive(false);
        }
    }

    public void BackToMenu()
    {
        StartCoroutine(PlaySoundOnce(ClickSound));
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    // plays sounds once
    public IEnumerator PlaySoundOnce(AudioClip clipSrc)
    {
        AudioSource audioSrc = GetComponent<AudioSource>();

        if (audioSrc.isPlaying)
        {
            yield return new WaitForSeconds(audioSrc.clip.length);
            audioSrc.PlayOneShot(clipSrc);
        }
        else if (!audioSrc.isPlaying)
            audioSrc.PlayOneShot(clipSrc);
    }
}
