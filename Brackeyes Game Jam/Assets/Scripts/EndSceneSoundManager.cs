using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneSoundManager : MonoBehaviour
{
    void Start()
    {
        RoomSound.instance.roomSound.Play();
        ForestNoise.instance.forestNoise.Stop();
    }
}
