using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _shot;

    private Rigidbody2D _rb;

    [SerializeField] private Transform _groundCheck;

    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private float _speed = 10f;

    [SerializeField] private float _jumpPower = 10f;

    float _jumpBufferTime = 0.2f;

    float _jumpBufferCounter;

    float _coyoteTime = 0.1f;

    float _coyoteTimeCounter;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();

        HandleJump();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void HandleMovement()
    {
        var moveX = Input.GetAxis("Horizontal");

        _rb.velocity = new Vector2(_speed * moveX, _rb.velocity.y);
    }

    void HandleJump()
    {
        if (IsGrounded())
        {
            _coyoteTimeCounter = _coyoteTime;
        }

        else
        {
            _coyoteTimeCounter -= Time.fixedDeltaTime;
        }

        if (Input.GetButtonDown("Jump")) 
        {
            _jumpBufferCounter = _jumpBufferTime;
        }

        _jumpBufferCounter -= Time.fixedDeltaTime;

        if (_jumpBufferCounter > 0f && _coyoteTimeCounter > 0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);

            _coyoteTimeCounter = 0f;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    void Shoot()
    {
        var shotDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        var shotAngle = Mathf.Atan2(shotDir.y, shotDir.x) * Mathf.Rad2Deg;

        var shot = Instantiate(_shot, transform.position, Quaternion.Euler(0f, 0f, shotAngle));

        shot.GetComponent<Shot>().dir = shotDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, 0.2f);
    }
}
