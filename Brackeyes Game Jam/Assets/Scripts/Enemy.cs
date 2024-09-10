using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask _topGround;

    [SerializeField] private Transform _topGroundTransform;

    private Transform _playerTransform;

    private Rigidbody2D _rb;

    private float _playerThresholdX = 0f;

    private float _speed = 7.5f;

    float _acceleration = 0.1f;

    float _deceleration = 0.4f;

    bool _canMoveRandomly = true;

    int _randomDir;

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
        if (_canMoveRandomly)
        {
            _randomDir = RandomDirection();

            _canMoveRandomly = false;
        }

        if (!OnTopGround()) Move(FollowPlayer());

        else Move(_randomDir);
    }

    void Move(float direction)
    {
        if (direction != 0) _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, _speed * direction, _acceleration), _rb.velocity.y);

        else _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, 0f, _deceleration), _rb.velocity.y);
    }

    int FollowPlayer()
    {
        int direction;

        float xDistanceToPlayer = _playerTransform.position.x - transform.position.x;

        if (Mathf.Abs(xDistanceToPlayer) > _playerThresholdX)
        {
            direction = (int)Mathf.Sign(xDistanceToPlayer);
        }

        else
        {
            direction = 0;
        }

        return direction;
    }

    int RandomDirection()
    {
        int randomDirection = Random.Range(-1, 2);

        while (randomDirection == 0) randomDirection = Random.Range(-1, 2);

        return randomDirection;

    }

    bool OnTopGround()
    {
        return Physics2D.BoxCast(_topGroundTransform.position, new Vector2(7f, 4f), 0f, Vector2.down, 2.5f, _topGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            Destroy(gameObject);
        }
    }
}
