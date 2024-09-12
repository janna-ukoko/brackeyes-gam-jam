using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Date : MonoBehaviour
{
    [SerializeField] CanvasGroup _date;

    private void Start()
    {
        _date.alpha = 0;
        StartCoroutine(FadeDate());
    }

    IEnumerator FadeDate()
    {
        yield return new WaitForSeconds(2f);

        LeanTween.alphaCanvas(_date, 1f, 2f);

        yield return new WaitForSeconds(4f);

        LeanTween.alphaCanvas(_date, 0f, 2f);

    }
}
