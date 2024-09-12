using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuneralSoundManager : MonoBehaviour
{
    void Start()
    {
        RoomSound.instance.roomSound.Stop();
    }
}
