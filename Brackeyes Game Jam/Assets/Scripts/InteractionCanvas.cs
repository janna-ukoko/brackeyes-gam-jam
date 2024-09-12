using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionCanvas : MonoBehaviour
{
    [SerializeField] private Image _blackOverlay;
    [SerializeField] private Image _funeralBackgroundImage;
    [SerializeField] private Image _brothersGravestone;

    private bool _canHideUI = false;

    public string promptMessage;

    public static bool playerCanMove = true;

    [SerializeField] private AudioSource _clickSound;

    public static InteractionCanvas instance;

    [SerializeField] private ChangeScene _changeScene;
    private bool _canUnsubscribe = true;

    bool _canPlay = true;

    private void Start()
    {
        Interactable.ObjectInteract += ShowUI;
    }

    private void Update()
    {
        if (_canHideUI && PauseMenu.canInteract)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                HideUI();
                _canHideUI = false;
                _clickSound.Play();
            }
        }

        if (_changeScene.sceneIsChanging && _canUnsubscribe)
        {
            Interactable.ObjectInteract -= ShowUI;
            _canUnsubscribe = false;
        }

        if (PauseMenu.gameIsPaused)
            BackgroundMusic.instance.backgroundMusic.Pause();
        else
            BackgroundMusic.instance.backgroundMusic.UnPause();

    }

    void ShowUI(object sender, EventArgs e)
    {
        LeanTween.alpha(_blackOverlay.GetComponent<RectTransform>(), 0.5f, 1f);

        if (promptMessage == "GraveBuilding")
        {
            LeanTween.moveY(_funeralBackgroundImage.GetComponent<RectTransform>(), 0f, 1f).setEaseOutBack();
            StartCoroutine(PlayAudio());
        }

        else if (promptMessage == "Gravestone" && _brothersGravestone != null)
            LeanTween.moveY(_brothersGravestone.GetComponent<RectTransform>(), -50f, 1f).setEaseOutBack();

        StartCoroutine(CanHideUI());

        PlayerMovement.InteractionUIIsActive = true;
    }

    IEnumerator CanHideUI()
    {
        yield return null;
        _canHideUI = true;
    }

    void HideUI()
    {
        LeanTween.alpha(_blackOverlay.GetComponent<RectTransform>(), 0f, 1f).setOnComplete(SetCanInteract);

        if (promptMessage == "GraveBuilding")
            LeanTween.moveY(_funeralBackgroundImage.GetComponent<RectTransform>(), -475f, 1f).setEaseInBack();
        else if (promptMessage == "Gravestone")
            LeanTween.moveY(_brothersGravestone.GetComponent<RectTransform>(), -520f, 1f).setEaseInBack();

        PlayerMovement.InteractionUIIsActive = false;
    }

    void SetCanInteract()
    {
        PlayerInteractionController.canInteract = true;
    }

    IEnumerator PlayAudio()
    {
        if (_canPlay)
        {
            yield return new WaitForSeconds(2f);
            BackgroundMusic.instance.backgroundMusic.Play();
            BackgroundMusic.instance.backgroundMusic.volume = 0.35f;
            _canPlay = false;
        }


    }

}
