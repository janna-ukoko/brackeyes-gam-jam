using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSoundManager : MonoBehaviour
{
    void Start()
    {
        RoomSound.instance.roomSound.Play();
        ForestNoise.instance.forestNoise.Stop();
        BackgroundMusic.instance.backgroundMusic.Stop();
    }

}
