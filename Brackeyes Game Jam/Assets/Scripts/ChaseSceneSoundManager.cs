using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSceneSoundManager : MonoBehaviour
{
    void Start()
    {
        RoomSound.instance.roomSound.Stop();
        ForestNoise.instance.forestNoise.Play();
        BackgroundMusic.instance.backgroundMusic.Stop();
    }
}
