using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _resumeButtonRect;
    [SerializeField] private RectTransform _exitGameButtonRect;

    [SerializeField] private CanvasGroup _resumeButtonCanvasGroup;
    [SerializeField] private CanvasGroup _exitGameButtonCanvasGroup;

    [SerializeField] private CanvasGroup _blackOverlay;

    public static bool gameIsPaused = false;
    public static bool canInteract = true;

    private bool _canPause = true;

    [SerializeField] private CanvasGroup _dialogueBox;

    [SerializeField] private float _displayTime = 0.75f;

    [SerializeField] private ChangeScene _changeScene;

    [SerializeField] private AudioSource _clickSound;
    [SerializeField] private AudioSource _selectSound;

    private bool _canPlayClickSound = false;

    public static bool buttonsAreInteractable = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _canPause)
        {
            if (!gameIsPaused)
            {
                PauseGame();
            }

            else
            {
                ResumeGame();
            }
        }

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

    public void PauseGame()
    {
        _clickSound.Play();

        gameIsPaused = true;
        StartCoroutine(SetCanInteractToFalse());
        _canPause = false;


        StartCoroutine(ShowUI());


        _resumeButtonCanvasGroup.interactable = true;
        _exitGameButtonCanvasGroup.interactable = true;

        if (_dialogueBox != null)
            _dialogueBox.alpha = 0;

        Time.timeScale = 0;

        SetButtonInteractabiltyToFalse();

    }

    public void ResumeGame()
    {
        _clickSound.Play();

        gameIsPaused = false;
        StartCoroutine(SetCanInteractToTrue());
        _canPause = false;

        StartCoroutine(HideUI());

        if (_dialogueBox != null)
            _dialogueBox.alpha = 1;

        Time.timeScale = 1;

        SetButtonInteractabiltyToFalse();

    }


    IEnumerator ShowUI()
    {
        LeanTween.alphaCanvas(_blackOverlay, 0.75f, _displayTime).setIgnoreTimeScale(true);

        LeanTween.moveY(_resumeButtonRect, _resumeButtonRect.anchoredPosition.y + 100, _displayTime).setEaseOutBack().setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(_resumeButtonCanvasGroup, 1, _displayTime).setIgnoreTimeScale(true).setOnComplete(SetCanPauseToTrue);

        yield return new WaitForSecondsRealtime(0.1f);

        LeanTween.moveY(_exitGameButtonRect, _exitGameButtonRect.anchoredPosition.y + 100, _displayTime).setEaseOutBack().setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(_exitGameButtonCanvasGroup, 1, _displayTime).setOnComplete(SetCanPauseToTrue).setIgnoreTimeScale(true).setOnComplete(SetButtonInteractabiltyToTrue);
    }

    IEnumerator HideUI()
    {
        LeanTween.alphaCanvas(_blackOverlay, 0f, _displayTime).setIgnoreTimeScale(true);

        LeanTween.moveY(_resumeButtonRect, _resumeButtonRect.anchoredPosition.y - 100, _displayTime).setEaseInBack().setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(_resumeButtonCanvasGroup, 0, _displayTime).setIgnoreTimeScale(true);

        yield return new WaitForSecondsRealtime(0.1f);

        LeanTween.moveY(_exitGameButtonRect, _exitGameButtonRect.anchoredPosition.y - 100, _displayTime).setEaseInBack().setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(_exitGameButtonCanvasGroup, 0, _displayTime).setOnComplete(SetCanPauseToTrue).setIgnoreTimeScale(true);

    }

    void SetCanPauseToTrue()
    {
        _canPause = true;

    }

    void SetButtonInteractabiltyToTrue()
    {
        _resumeButtonCanvasGroup.interactable = true;
        _exitGameButtonCanvasGroup.interactable = true;

        buttonsAreInteractable = true;
    }

    void SetButtonInteractabiltyToFalse()
    {
        _resumeButtonCanvasGroup.interactable = false;
        _exitGameButtonCanvasGroup.interactable = false;

        buttonsAreInteractable = false;
    }

    public void ExitGame()
    {
        _clickSound.Play();
        Application.Quit();
    }

    IEnumerator SetCanInteractToTrue()
    {
        yield return null;
        canInteract = true;
    }

    IEnumerator SetCanInteractToFalse()
    {
        canInteract = false;
        yield return null;
    }
}
