using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _shot;

    [SerializeField] private Transform _shotOrigin;

    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _characterRig;

    [SerializeField] private Transform _rightArm;

    [SerializeField] private Transform _rightArmPivot;

    [SerializeField] private Transform _rightForearmPivot;

    private Rigidbody2D _rb;

    [SerializeField] private Transform _groundCheck;

    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private float _speed = 10f;

    [SerializeField] private float _jumpPower = 10f;

    private float _maxArmExtension = 2f;

    float _jumpBufferTime = 0.2f;

    float _jumpBufferCounter;

    float _coyoteTime = 0.1f;

    float _coyoteTimeCounter;

    float _moveX;

    float _bodyDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }

        Aim();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();

        HandleJump();

        HandleAnimations();
    }

    void HandleMovement()
    {
        _rb.velocity = new Vector2(_speed * _moveX, _rb.velocity.y);
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

    Vector3 GetMousePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        return mousePos;
    }

    void Aim()
    {
        _rightArm.position = GetMousePosition();

        _bodyDir = Mathf.Sign(GetMousePosition().x - transform.position.x);

        _characterRig.localScale = new Vector2(_bodyDir, _characterRig.localScale.y);

        if (Vector3.Distance(_rightArm.position, _rightArmPivot.position) > _maxArmExtension)
        {
            Vector3 dir = (GetMousePosition() - _rightArmPivot.position).normalized;

            _rightArm.position = _rightArmPivot.position + dir * _maxArmExtension;
        }
    }

    void Shoot()
    {
        var shotDir = (GetMousePosition() - _rightForearmPivot.position).normalized;

        var shotAngle = Mathf.Atan2(shotDir.y, shotDir.x) * Mathf.Rad2Deg;

        var shot = Instantiate(_shot, _shotOrigin.position, Quaternion.Euler(0f, 0f, shotAngle));

        shot.GetComponent<Shot>().dir = shotDir;
    }

    void HandleAnimations()
    {
        if (IsGrounded() && _moveX < 0.1f && _moveX > -0.1f)
        {
            _animator.SetInteger("state", 0);
        }

        else if (IsGrounded() && ((_moveX > 0.1f && _bodyDir == 1f) || (_moveX < -0.1f && _bodyDir == -1f)))
        {
            _animator.SetInteger("state", 1);
        }

        else if (IsGrounded() && ((_moveX > 0.1f && _bodyDir == -1f) || (_moveX < -0.1f && _bodyDir == 1f)))
        {
            _animator.SetInteger("state", 2);
        }

        else if (!IsGrounded() && _rb.velocity.y > 0f)
        {

        }

        else if (!IsGrounded() && _rb.velocity.y < 0f)
        {

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, 0.2f);
    }
}