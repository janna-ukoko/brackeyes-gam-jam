using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    [SerializeField] private NoiseSettings _idleNoise;
    
    [SerializeField] private NoiseSettings _cameraShakeNoise;

    private CinemachineBasicMultiChannelPerlin _noise;

    private float _cameraShakeTime = 0.5f;

    private void Start()
    {
        _noise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake()
    {
        _noise.m_NoiseProfile = _cameraShakeNoise;

        StartCoroutine(ShakeCamera());
    }

    private IEnumerator ShakeCamera()
    {
        yield return new WaitForSeconds(_cameraShakeTime);

        _noise.m_NoiseProfile = _idleNoise;
    }
}
