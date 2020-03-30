using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas MainCanvas;
    public Canvas CreditCanvas;
    public Canvas OptionsCanvas;

    public AudioClip ClickSound;

    private void Start()
    {
        MainCanvas.gameObject.SetActive(true);
        CreditCanvas.gameObject.SetActive(false);
        OptionsCanvas.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        StartCoroutine(PlaySoundOnce(ClickSound));
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void OptionsMenu(bool change)
    {
        StartCoroutine(PlaySoundOnce(ClickSound));
        if (change)
        {
            MainCanvas.gameObject.SetActive(false);
            CreditCanvas.gameObject.SetActive(false);
            OptionsCanvas.gameObject.SetActive(true);
        }
        else
        {
            MainCanvas.gameObject.SetActive(true);
            CreditCanvas.gameObject.SetActive(false);
            OptionsCanvas.gameObject.SetActive(false);
        }
    }

    public void CreditScreen(bool change)
    {
        StartCoroutine(PlaySoundOnce(ClickSound));
        if (change)
        {
            MainCanvas.gameObject.SetActive(false);
            CreditCanvas.gameObject.SetActive(true);
            OptionsCanvas.gameObject.SetActive(false);
        }
        else
        {
            MainCanvas.gameObject.SetActive(true);
            CreditCanvas.gameObject.SetActive(false);
            OptionsCanvas.gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        StartCoroutine(PlaySoundOnce(ClickSound));
        Debug.Log("Quit Game");
        Application.Quit();
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
