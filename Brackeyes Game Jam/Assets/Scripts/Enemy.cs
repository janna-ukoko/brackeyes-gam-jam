using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _playerTransform;

    private Rigidbody2D _rb;

    private float _playerThreshold = 2.5f;

    private float _speed = 7.5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        var xDistanceToPlayer = _playerTransform.position.x - transform.position.x;

        if (Mathf.Abs(xDistanceToPlayer) > _playerThreshold)
        {
            _rb.velocity = new Vector2(_speed * Mathf.Sign(xDistanceToPlayer), _rb.velocity.y);
        }

        else
        {
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            Destroy(gameObject);
        }
    }
}
