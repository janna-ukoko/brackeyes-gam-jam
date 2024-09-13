using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private float _playerThreshold = 3f;

    private SpriteRenderer _spriteRenderer;

    private float _alphaValue = 1f;

    private float _fadeValue = 0.2f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        _spriteRenderer.color = new Vector4(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _alphaValue);

        if (Mathf.Abs(transform.position.x - _player.position.x) < _playerThreshold) FadeAlphaOut();

        else FadeAlphaIn();
    }

    void FadeAlphaOut()
    {
        _alphaValue = Mathf.Lerp(_alphaValue, 0.4f, _fadeValue);
    }

    void FadeAlphaIn()
    {
        _alphaValue = Mathf.Lerp(_alphaValue, 1f, _fadeValue);
    }
}
