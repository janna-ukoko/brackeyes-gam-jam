using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private Animator _characterRigAnimator;

    [SerializeField] private bool _canBlink = true;

    [SerializeField] private float _blinkDuration = 0.25f;

    [SerializeField] private float _minWaitTime = 4f;
    [SerializeField] private float _maxWaitTime = 6f;


    private void Start()
    {
        StartCoroutine("BlinkEyes");
    }


    private IEnumerator BlinkEyes()
    {
        while (true && _canBlink)
        {
            float waitTime = Random.Range(_minWaitTime, _maxWaitTime);

            yield return new WaitForSeconds(waitTime);

            _characterRigAnimator.SetLayerWeight(2, 0.25f);

            yield return new WaitForSeconds(_blinkDuration / 4);

            _characterRigAnimator.SetLayerWeight(2, 0.5f);

            yield return new WaitForSeconds(_blinkDuration / 4);

            _characterRigAnimator.SetLayerWeight(2, 0.75f);

            yield return new WaitForSeconds(_blinkDuration / 4);

            _characterRigAnimator.SetLayerWeight(2, 1);

            _characterRigAnimator.SetLayerWeight(3, 1);

            yield return new WaitForSeconds(_blinkDuration / 2);

            _characterRigAnimator.SetLayerWeight(3, 0);

            _characterRigAnimator.SetLayerWeight(2, 0.75f);

            yield return new WaitForSeconds(_blinkDuration / 4);

            _characterRigAnimator.SetLayerWeight(2, 0.5f);

            yield return new WaitForSeconds(_blinkDuration / 4);

            _characterRigAnimator.SetLayerWeight(2, 0.25f);

            yield return new WaitForSeconds(_blinkDuration / 4);

            _characterRigAnimator.SetLayerWeight(2, 0);


        }

    }
}
