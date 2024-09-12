using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private float _scaledown = 0.75f;
    [SerializeField] private float _scaleUp = 0.9f;

    void Start()
    {
        ScaleDown();
    }

    void ScaleDown()
    {
        LeanTween.scale(this.gameObject, new Vector3(_scaledown, _scaledown, _scaledown), 0.75f).setOnComplete(ScaleUp);
    }

    void ScaleUp()
    {
        LeanTween.scale(this.gameObject, new Vector3(_scaleUp, _scaleUp, _scaleUp), 0.75f).setOnComplete(ScaleDown);
    }
}
