using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _distanceThreshold = 1.0f;

    [SerializeField] private float _scale = 1f;

    private void Start()
    {

        this.transform.localScale = Vector3.zero;
    }

    private void FixedUpdate()
    {
        var distanceFromPlayer = transform.position.x - _playerTransform.position.x;

        if (Mathf.Abs(distanceFromPlayer) <= _distanceThreshold)
        {
            this.transform.localScale = Vector3.one * _scale;
            //LeanTween.scale(this.gameObject, Vector3.one, 1f).setEaseInOutElastic();

        }

        else
        {
            this.transform.localScale = Vector3.zero;
            //LeanTween.scale(this.gameObject, Vector3.zero, 1f).setEaseInOutElastic();

        }

    }
}
