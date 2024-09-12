using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public RectTransform nameRect;
    public RectTransform dialogueRect;
    public RectTransform XButtonRect;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public GameObject XButton;
    public RectTransform dialogueBox;
    public CanvasGroup dialogueBoxCanvasGroup;

    public float speed = 1.0f;
    public float typingSpeed = 0.2f;

    public float dialogueOpenYPos = -142.49f;
    public float dialogueCloseYPos = -291.5f;

    [SerializeField] private YoungerBrotherMovement NPC;

    private Queue<string> names;
    private Queue<string> sentences;

    [SerializeField] private AudioSource _clickSound;

    private int _dialogueIndex = 0;
    private bool _canShowGift = true;
    private bool _canHideGift = true;

    void Start()
    {
        names = new Queue<string>();
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (XButton.activeInHierarchy == true && PauseMenu.canInteract)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                DisplayNextName();
                DisplayNextSentence();

                _clickSound.Play();

                _dialogueIndex++;
            }

        }

        if (_dialogueIndex == 2 && _canShowGift)
        {
            StartCoroutine(NPC.ShowGift());
            _canShowGift = false;
        }

        if (_dialogueIndex == 3 && _canHideGift)
        {
            StartCoroutine(NPC.HideGift());
            NPC.SetDisappointedFace();
            _canHideGift = false;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        LeanTween.alphaCanvas(dialogueBoxCanvasGroup, 1, speed).setEaseInOutBack();
        LeanTween.moveY(dialogueBox, dialogueOpenYPos, speed).setEaseInOutBack();

        names.Clear();

        sentences.Clear();

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextName();

        DisplayNextSentence();

    }

    public void DisplayNextName()
    {
        if (names.Count == 0)
        {
            EndDialogue();
            return;
        }

        nameRect.anchoredPosition = new Vector2(nameRect.anchoredPosition.x * -1, nameRect.anchoredPosition.y);
        dialogueRect.anchoredPosition = new Vector2(dialogueRect.anchoredPosition.x * -1, dialogueRect.anchoredPosition.y);
        XButtonRect.anchoredPosition = new Vector2(XButtonRect.anchoredPosition.x * -1, XButtonRect.anchoredPosition.y);

        string name = names.Dequeue();

        nameText.text = name;
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";

        XButton.SetActive(false);

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        XButton.SetActive(true);
    }

    void EndDialogue()
    {
        LeanTween.alphaCanvas(dialogueBoxCanvasGroup, 0, speed).setEaseInOutBack();
        LeanTween.moveY(dialogueBox, dialogueCloseYPos, speed).setEaseInOutBack();

        XButton.SetActive(false);

        StartCoroutine(NPC.LeaveScene());
    }


}
