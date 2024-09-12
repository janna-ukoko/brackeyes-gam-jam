using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;

    [SerializeField] private RectTransform _playButtonRect;
    [SerializeField] private RectTransform _exitButtonRect;

    [SerializeField] private CanvasGroup _mainTitle;
    [SerializeField] private CanvasGroup _subTitle;

    [SerializeField] private CanvasGroup _playButtonCanvasGroup;
    [SerializeField] private CanvasGroup _exitButtonCanvasGroup;

    [SerializeField] private CanvasGroup _gameMenu;

    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;

    [SerializeField] private AudioSource _clickSound;
    [SerializeField] private AudioSource _selectSound;

    private bool _canPlayClickSound = false;

    private bool buttonsAreInteractable = false;

    private void Start()
    {
        _mainTitle.alpha = 0;
        _subTitle.alpha = 0;
        _playButtonCanvasGroup.alpha = 0;
        _exitButtonCanvasGroup.alpha = 0;

        StartCoroutine(FadeUIIn());
    }

    void Update()
    {
        var vInput = Input.GetAxisRaw("Vertical");

        if (vInput != 0)
        {
            if (_canPlayClickSound)
            {
                if (buttonsAreInteractable)
                {
                    _selectSound.Play();
                    _canPlayClickSound = false;
                }

            }
        }

        else
        {
            _canPlayClickSound = true;
        }
    }

    IEnumerator FadeUIIn()
    {
        yield return new WaitForSeconds(3f);
        LeanTween.alphaCanvas(_mainTitle, 1, 2f);
        yield return new WaitForSeconds(2f);
        LeanTween.alphaCanvas(_subTitle, 1, 2f);
        yield return new WaitForSeconds(4f);
        LeanTween.alphaCanvas(_playButtonCanvasGroup, 1, 2f);
        LeanTween.alphaCanvas(_exitButtonCanvasGroup, 1, 2f).setOnComplete(SetButtonInteractabiltyToTrue);
    }

    public void playGame()
    {
        _clickSound.Play();

        _player.gameHasStarted = true;

        _playButtonCanvasGroup.interactable = false;
        _exitButtonCanvasGroup.interactable = false;

        buttonsAreInteractable = false;

        StartCoroutine(FadeUIOut());

    }

    IEnumerator FadeUIOut()
    {
        LeanTween.alphaCanvas(_mainTitle, 0, 2f);

        LeanTween.moveY(_playButtonRect, _playButtonRect.anchoredPosition.y - 100, 2f).setEaseInOutBack();
        LeanTween.alphaCanvas(_playButtonCanvasGroup, 0, 1f);

        yield return new WaitForSeconds(0.1f);

        LeanTween.moveY(_exitButtonRect, _exitButtonRect.anchoredPosition.y - 100, 2f).setEaseInOutBack();
        LeanTween.alphaCanvas(_exitButtonCanvasGroup, 0, 1f).setOnComplete(ShowControls);

        yield return new WaitForSeconds(1f);

        LeanTween.alphaCanvas(_subTitle, 0, 2f);
    }

    public void exitGame()
    {
        _clickSound.Play();

        Application.Quit();
    }

    void SetButtonInteractabiltyToTrue()
    {
        _playButtonCanvasGroup.interactable = true;
        _exitButtonCanvasGroup.interactable = true;

        buttonsAreInteractable = true;
        _selectSound.Play();
    }

    void ShowControls()
    {
        LeanTween.alpha(leftButton, 1, 2f);
        LeanTween.alpha(rightButton, 1, 2f);
    }


}
