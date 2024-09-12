using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private float _minBoundsX;
    [SerializeField] private float _maxBoundsX;

    private void Start()
    {
        transform.position = new Vector2(_minBoundsX, transform.position.y);
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(Mathf.Clamp(_playerTransform.position.x, _minBoundsX, _maxBoundsX),
                                         _playerTransform.position.y);
        
    }
}
