using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestNoise : MonoBehaviour
{
    [SerializeField] public AudioSource forestNoise;

    public static ForestNoise instance;

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
            forestNoise.Pause();
        else
            forestNoise.UnPause();

    }

}