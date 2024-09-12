using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public enum FacialExpressions { defaultFace, disappointed, smile, surprise, sad }
    public enum State { walk, run }

    [SerializeField] private FacialExpressions _facialExpression = FacialExpressions.defaultFace;
    [SerializeField] private State _state = State.run;

    [SerializeField] private bool _isResting;

    [SerializeField] private Animator _animator;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _characterRigTransform;

    private float _groundCheckDistance = 0.4f;

    [SerializeField] private float _runSpeed = 10;
    [SerializeField] private float _walkSpeed = 5;
    [SerializeField] private float _jumpVelocity = 25;

    [SerializeField] private LayerMask _jumpLayer;
    [SerializeField] private Transform _groundCheck;

    public int moveDir { get; private set; } = 0;

    [SerializeField] private float _minFaceChangeRate = 1f;
    [SerializeField] private float _maxFaceChangeRate = 3f;

    private bool canMove = false;
    [SerializeField] private float _movementDelayTime = 0f;

    public bool gameHasStarted = true;

    public static bool InteractionUIIsActive = false;

    public bool isStopped = false;

    [SerializeField] private AudioSource _runningSound;
    [SerializeField] private AudioSource _walkingSound;
    private bool _canPlayWalkingOrRunningSound = true;

    private void Start()
    {
        if (_isResting) StartCoroutine(ChangeFacialExpressions());

        StartCoroutine(DelayMovement());

    }

    private void Update()
    {
        if (PauseMenu.gameIsPaused)
        {
            if (_state == State.run)
                _runningSound.Pause();
            else
                _walkingSound.Pause();
        }

        else if (!PauseMenu.gameIsPaused)
        {
            if (_state == State.run)
                _runningSound.UnPause();
            else
                _walkingSound.UnPause();
        }
    }

    void FixedUpdate()
    {
        if (InteractionCanvasRoom.gameIsEnding)
            _facialExpression = FacialExpressions.disappointed;

        if (InteractionUIIsActive)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);

            moveDir = 0;

            if (_state == State.run)
                _runningSound.Stop();
            else
                _walkingSound.Stop();
        }

        if (canMove && gameHasStarted && !InteractionUIIsActive && !isStopped)
        {
            if (!_isResting)
            {
                HandleMovement();

                _animator.SetBool("isResting", false);
            }
            else
            {
                _animator.SetBool("isResting", true);
            }
        }

        HandleAnimations();

        if (InteractionUIIsActive) _canPlayWalkingOrRunningSound = true;
    }

    IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(_movementDelayTime);
        canMove = true;
    }

    IEnumerator ChangeFacialExpressions()
    {
        var enumLength = Enum.GetNames(typeof(FacialExpressions)).Length;

        while (true)
        {
            _facialExpression = (FacialExpressions)UnityEngine.Random.Range(0, enumLength);

            while (_facialExpression == FacialExpressions.sad || _facialExpression == FacialExpressions.disappointed)
                _facialExpression = (FacialExpressions)UnityEngine.Random.Range(0, enumLength);

            yield return new WaitForSeconds(UnityEngine.Random.Range(_minFaceChangeRate, _maxFaceChangeRate));
        }

    }

    void HandleMovement()
    {
        float xInput = Input.GetAxis("Horizontal");

        float speed = _state == State.run ? _runSpeed : _walkSpeed;

        _rb.velocity = new Vector2(xInput * speed, _rb.velocity.y);

        if (xInput < 0)
        {
            _characterRigTransform.localScale = new Vector2(-1, _characterRigTransform.localScale.y);
            moveDir = -1;


            if (_canPlayWalkingOrRunningSound)
            {
                if (_state == State.run)
                    _runningSound.Play();
                else
                    _walkingSound.Play();

                _canPlayWalkingOrRunningSound = false;
            }
        }

        else if (xInput > 0)
        {
            _characterRigTransform.localScale = new Vector2(1, _characterRigTransform.localScale.y);
            moveDir = 1;

            if (_canPlayWalkingOrRunningSound)
            {
                if (_state == State.run)
                    _runningSound.Play();
                else
                    _walkingSound.Play();

                _canPlayWalkingOrRunningSound = false;
            }
        }

        else
        {
            moveDir = 0;

            if (_state == State.run)
                _runningSound.Stop();
            else
                _walkingSound.Stop();

            _canPlayWalkingOrRunningSound = true;
        }


    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpVelocity);
        }
    }

    bool isGrounded()
    {
        return Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _jumpLayer);
    }


    void HandleAnimations()
    {
        if (isGrounded())
            _animator.SetInteger("state", Mathf.Abs(moveDir) * ((int)_state +1));
        else
            _animator.SetInteger("state", 3);

        _animator.SetInteger("facialExpression", (int)_facialExpression);
    }

}
