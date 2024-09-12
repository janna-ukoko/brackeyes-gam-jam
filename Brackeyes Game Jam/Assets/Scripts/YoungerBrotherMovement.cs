using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoungerBrotherMovement : MonoBehaviour
{
    public enum State { walk, run }
    public enum FacialExpressions { defaultFace, disappointed, smile, surprise, sad }

    [SerializeField] private State _state = State.walk;
    [SerializeField] private FacialExpressions _facialExpression = FacialExpressions.defaultFace;

    [SerializeField] public Animator animator;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Transform _characterRigTransform;

    [SerializeField] private float _runSpeed = 10;
    [SerializeField] private float _walkSpeed = 5;

    [SerializeField] private float _entryWaitTime = 3f;
    [SerializeField] private float _exitWaitTime = 3f;

    public int direction { private set; get; } = 0;
    [SerializeField] private int _startDirection = -1;

    public bool canMove = false;

    [SerializeField] private bool _isAutonomous = true;

    [SerializeField] private AudioSource _walkingSound;
    [SerializeField] private AudioSource _runningSound;
    private bool _canPlayWalkingOrRunningSound = false;

    private float _showGiftSpeed = 0.2f;

    private void Start()
    {
        if (_isAutonomous)
            StartCoroutine(BeginMovement());

        direction = _startDirection;
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
        if (canMove)
        {
            HandleMovement();

            if (_canPlayWalkingOrRunningSound)
            {

                if (_canPlayWalkingOrRunningSound)
                {
                    if (_state == State.run)
                        _runningSound.Play();
                    else
                        _walkingSound.Play();

                    _canPlayWalkingOrRunningSound = false;
                }

                _canPlayWalkingOrRunningSound = false;
            }
        }

        else
        {
            direction = 0;

            if (_state == State.run)
                _runningSound.Stop();
            else
                _walkingSound.Stop();

            _canPlayWalkingOrRunningSound = true;
        }

        HandleAnimations();

    }

    public IEnumerator BeginMovement()
    {
        yield return new WaitForSeconds(_entryWaitTime);

        direction = _startDirection;

        canMove = true;

        _canPlayWalkingOrRunningSound = true;

        //yield return new WaitForSeconds(2f);

        //if (_state == State.run)
        //    _runningSound.Stop();
        //else
        //    _walkingSound.Stop();
    }

    void HandleMovement()
    {
        float speed = _state == State.run ? _runSpeed : _walkSpeed;

        _rb.velocity = new Vector2(direction * speed, _rb.velocity.y);

        if (direction != 0)
            _characterRigTransform.localScale = new Vector2(direction, _characterRigTransform.localScale.y);

    }

    void HandleAnimations()
    {
        animator.SetInteger("state", Mathf.Abs(direction) * ((int)_state + 1));

        animator.SetInteger("facialExpression", (int)_facialExpression);
    }

    public void SetDisappointedFace()
    {
        _facialExpression = FacialExpressions.disappointed;
    }

    public IEnumerator ShowGift()
    {
        animator.SetLayerWeight(4, 0.1f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.2f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.3f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.4f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.5f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.6f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.7f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.8f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.9f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 1.0f);


    }

    public IEnumerator HideGift()
    {
        animator.SetLayerWeight(4, 0.9f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.8f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.7f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.6f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.5f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.4f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.3f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.2f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.1f);
        yield return new WaitForSeconds(_showGiftSpeed / 10);
        animator.SetLayerWeight(4, 0.1f);
    }

    public IEnumerator LeaveScene()
    {
        yield return new WaitForSeconds(_exitWaitTime);
        canMove = true;
        direction = 1;
    }
}
