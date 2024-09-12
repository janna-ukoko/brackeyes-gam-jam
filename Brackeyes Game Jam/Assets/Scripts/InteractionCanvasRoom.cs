using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class InteractionCanvasRoom : MonoBehaviour
{
    [SerializeField] private Image _blackOverlay;
    [SerializeField] private Image _familyPhoto;
    [SerializeField] private Image _brothersGift;

    [SerializeField] private GameObject _interactButtonBrothersGift;

    private bool _canHideUI = false;

    public string promptMessage;

    public static bool gameIsEnding = false;

    [SerializeField] private ChangeScene _changeScene;
    [SerializeField] private PlayerMovement _player;

    [SerializeField] private Animator _vCamAnimator;

    [SerializeField] private AudioSource _clickSound;

    public static InteractionCanvasRoom instance;

    private bool _canUnsubscribe = true;

    private void Start()
    {
        InteractableRoom.ObjectInteract += ShowUI;
    }

    private void Update()
    {
        if (_canHideUI)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                HideUI();
                _canHideUI = false;
                _clickSound.Play();

                if (promptMessage == "BrothersGift")
                {
                    StartCoroutine(EndGame());
                    _player.isStopped = true;
                }

            }
        }


        if (_changeScene.sceneIsChanging && _canUnsubscribe)
        {
            InteractableRoom.ObjectInteract -= ShowUI;
            _canUnsubscribe = false;
        }

    }

    void ShowUI(object sender, EventArgs e)
    {
        LeanTween.alpha(_blackOverlay.GetComponent<RectTransform>(), 0.5f, 1f);

        if (promptMessage == "FamilyPhoto")
            LeanTween.moveY(_familyPhoto.GetComponent<RectTransform>(), 0f, 1f).setEaseOutBack();
        else if (promptMessage == "BrothersGift")
            LeanTween.moveY(_brothersGift.GetComponent<RectTransform>(), 0f, 1f).setEaseOutBack();

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

        if (promptMessage == "FamilyPhoto")
            LeanTween.moveY(_familyPhoto.GetComponent<RectTransform>(), -470f, 1f).setEaseInBack();
        else if (promptMessage == "BrothersGift")
            LeanTween.moveY(_brothersGift.GetComponent<RectTransform>(), -470f, 1f).setEaseInBack();

        PlayerMovement.InteractionUIIsActive = false;
    }

    void SetCanInteract()
    {
        PlayerInteractionController.canInteract = true;
    }

    IEnumerator EndGame()
    {
        _interactButtonBrothersGift.SetActive(false);
        _vCamAnimator.SetBool("gameHasEnded", true);
        yield return new WaitForSeconds(4f);
        gameIsEnding = true;
        yield return new WaitForSeconds(4f);
        StartCoroutine(_changeScene.FadeToNewScene());
    }

}
