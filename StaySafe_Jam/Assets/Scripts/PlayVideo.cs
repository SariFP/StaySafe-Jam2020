using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    [Space(5)]
    [Header("DeliveryVideo")]
    public RawImage VideoRawImage;
    public VideoPlayer videoPlayer;
    public AudioSource videoAudio;
    private AudioSource mainAudio;

    void Start()
    {
        mainAudio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        StartCoroutine(StartVideo());
    }

    IEnumerator StartVideo()
    {
        mainAudio.Stop();
        videoPlayer.Prepare();
        WaitForSeconds wait = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return wait;
            break;
        }
        VideoRawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        videoAudio.Play();

        yield return new WaitForSeconds(26);
        mainAudio.Play();
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);

    }
}
