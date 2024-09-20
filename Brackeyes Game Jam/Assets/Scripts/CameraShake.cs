using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;

    private CinemachineBasicMultiChannelPerlin _noise;

    private float _cameraShakeTime = 0.5f;

    private void Start()
    {
        _virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>(); 

        _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake()
    {
        _noise.m_AmplitudeGain = 1.5f;

        _noise.m_FrequencyGain = 4.5f;

        StartCoroutine(ShakeCamera());
    }

    private IEnumerator ShakeCamera()
    {
        yield return new WaitForSeconds(_cameraShakeTime);

        _noise.m_AmplitudeGain = 0.75f;

        _noise.m_FrequencyGain = 0.15f;
    }
}
