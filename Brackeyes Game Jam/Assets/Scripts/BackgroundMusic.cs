using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public static BackgroundMusic instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (PauseMenu.gameIsPaused)
            backgroundMusic.Pause();
        else
            backgroundMusic.UnPause();

        if (InteractionCanvasRoom.gameIsEnding)
        {
            ReduceVolume();
        }
    }

    public void ReduceVolume()
    {
        if (backgroundMusic.volume > 0)
        {
            backgroundMusic.volume -= Time.deltaTime /5;
        }
    }
}
