using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneSoundManager : MonoBehaviour
{
    void Start()
    {
        RoomSound.instance.roomSound.Stop();
        ForestNoise.instance.forestNoise.Play();
        BackgroundMusic.instance.backgroundMusic.Stop();
    }
}
