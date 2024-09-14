using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] Vector2 _parallaxEffectMultiplier = new Vector2(1, 1);

    private Vector3 _initialPosition;
    
    private Vector3 _cameraInitialPosition;
    
    private Vector3 _movementChanges;

    private void Start()
    {
        _initialPosition = transform.position;
        
        _cameraInitialPosition = GetCameraPosition();
    }

    private void LateUpdate()
    {
        _movementChanges = GetCameraPosition() - _cameraInitialPosition;

        transform.position = _initialPosition + new Vector3(_movementChanges.x * _parallaxEffectMultiplier.x,
                                                            _movementChanges.y * _parallaxEffectMultiplier.y,
                                                            _movementChanges.z);
    }

    private Vector3 GetCameraPosition()
    {
        var pos = Camera.main.transform.position;
        pos.z = 0f;

        return pos;
    }
}
