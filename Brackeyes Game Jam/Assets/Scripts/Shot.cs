using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Rigidbody2D _rb;

    private SpriteRenderer _sprite;

    private BoxCollider2D _collider;

    public Vector2 dir;

    [SerializeField] private float _speed = 10f;

    private float _destructiontime = 2f;

    private bool _destructionHasStarted = false;

    [SerializeField] private AudioSource _bulletHitAudio;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _sprite = GetComponent<SpriteRenderer>();

        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = dir * _speed;
    }

    private void OnBecameInvisible()
    {
        DestroyShot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _bulletHitAudio.Play();

        DestroyShot();
    }

    void DestroyShot()
    {
        if (!_destructionHasStarted)
        {
            StartCoroutine(DestroyObjectCoroutine());

            _destructionHasStarted = true;

            _rb.velocity = new Vector2();
            
            _sprite.enabled = false;

            _collider.enabled = false;
        }
    }

    IEnumerator DestroyObjectCoroutine()
    {
        yield return new WaitForSeconds(_destructiontime);

        Destroy(gameObject);
    }
}
