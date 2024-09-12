using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Messages : MonoBehaviour
{
    [SerializeField] GameObject _dialogue1;
    [SerializeField] GameObject _dialogue2;
    [SerializeField] GameObject _dialogue3;
    [SerializeField] GameObject _dialogue4;

    SpriteRenderer _dialogue1SpriteRenderer;
    SpriteRenderer _dialogue2SpriteRenderer;
    SpriteRenderer _dialogue3SpriteRenderer;
    SpriteRenderer _dialogue4SpriteRenderer;

    [SerializeField] AudioSource _notificationSound;

    private void Start()
    {
        _dialogue1SpriteRenderer = _dialogue1.GetComponent<SpriteRenderer>();
        _dialogue2SpriteRenderer = _dialogue2.GetComponent<SpriteRenderer>();
        _dialogue3SpriteRenderer = _dialogue3.GetComponent<SpriteRenderer>();
        _dialogue4SpriteRenderer = _dialogue4.GetComponent<SpriteRenderer>();

        _dialogue1SpriteRenderer.color = new Color(_dialogue1SpriteRenderer.color.r, _dialogue1SpriteRenderer.color.g, _dialogue1SpriteRenderer.color.b, 0);
        _dialogue2SpriteRenderer.color = new Color(_dialogue2SpriteRenderer.color.r, _dialogue2SpriteRenderer.color.g, _dialogue2SpriteRenderer.color.b, 0);
        _dialogue3SpriteRenderer.color = new Color(_dialogue3SpriteRenderer.color.r, _dialogue3SpriteRenderer.color.g, _dialogue3SpriteRenderer.color.b, 0);
        _dialogue4SpriteRenderer.color = new Color(_dialogue4SpriteRenderer.color.r, _dialogue4SpriteRenderer.color.g, _dialogue4SpriteRenderer.color.b, 0);

        StartCoroutine(DisplayMessages());    
    }

    private void Update()
    {
        if (PauseMenu.gameIsPaused)
            _notificationSound.Pause();
        else
            _notificationSound.UnPause();
    }

    IEnumerator DisplayMessages()
    {
        yield return new WaitForSeconds(8f);

        _notificationSound.Play();
        yield return new WaitForSeconds(1f);

        LeanTween.alpha(_dialogue1, 1f, 1f);
        LeanTween.moveY(_dialogue1, _dialogue1.transform.position.y + 2.0f, 3.5f);
        yield return new WaitForSeconds(2.5f);
        LeanTween.alpha(_dialogue1, 0f, 1f);

        yield return new WaitForSeconds(1.5f);

        _notificationSound.Play();
        yield return new WaitForSeconds(1f);

        LeanTween.alpha(_dialogue2, 1f, 1f);
        LeanTween.moveY(_dialogue2, _dialogue2.transform.position.y + 2.0f, 3.5f);
        yield return new WaitForSeconds(2.5f);
        LeanTween.alpha(_dialogue2, 0f, 1f);

        yield return new WaitForSeconds(1f);

        _notificationSound.Play();
        yield return new WaitForSeconds(1f);

        LeanTween.alpha(_dialogue3, 1f, 1f);
        LeanTween.moveY(_dialogue3, _dialogue3.transform.position.y + 2.0f, 3.5f);
        yield return new WaitForSeconds(2.5f);
        LeanTween.alpha(_dialogue3, 0f, 1f);

        yield return new WaitForSeconds(2f);

        _notificationSound.Play();
        yield return new WaitForSeconds(1f);

        LeanTween.alpha(_dialogue4, 1f, 1f);
        LeanTween.moveY(_dialogue4, _dialogue4.transform.position.y + 2.0f, 3.5f);
        yield return new WaitForSeconds(2.5f);
        LeanTween.alpha(_dialogue4, 0f, 1f);

    }
}
