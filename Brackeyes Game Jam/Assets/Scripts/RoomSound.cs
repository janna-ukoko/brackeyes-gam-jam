using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSound : MonoBehaviour
{
    [SerializeField] public AudioSource roomSound;

    public static RoomSound instance;

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

    private void Update()
    {
        if (PauseMenu.gameIsPaused)
            roomSound.Pause();
        else
            roomSound.UnPause();

    }
}
