using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public MusicManager Instance;
    private AudioSource music;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            DestroyImmediate(gameObject);

        music = this.GetComponent<AudioSource>();
    }

    public void SetMusicVolume(float vol)
    {
        music.volume = vol;
    }
}
