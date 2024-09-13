using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private float _playerThreshold = 2f;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - _player.position.x) < _playerThreshold) FadeOut();

        else FadeIn();
    }

    void FadeOut()
    {
        _spriteRenderer.color = new Vector4(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0f) ;
    }

    void FadeIn()
    {
        _spriteRenderer.color = new Vector4(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f);
    }
}
