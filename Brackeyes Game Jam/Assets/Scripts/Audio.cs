using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource _footStepAudio;

    [SerializeField] private AudioSource _landingAudio;

    public void PlayFootStepAudio()
    {
        _footStepAudio.Play();
    }

    public void PlayLandingAudio()
    {
        _landingAudio.Play();
    }
}
