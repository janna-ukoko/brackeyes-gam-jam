using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private Interactable _interactable;

    private InteractableRoom _interactableRoom;

    public static bool canInteract = true;

    [SerializeField] private InteractionCanvas _interactionCanvas;

    [SerializeField] private InteractionCanvasRoom _interactionCanvasRoom;

    [SerializeField] private AudioSource _clickSound;

    void FixedUpdate()
    {
        if (_interactable != null && canInteract)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                _interactable.BaseInteraction();
                canInteract = false;
                _clickSound.Play();
            }
        }

        else if (_interactableRoom != null && canInteract)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                _interactableRoom.BaseInteraction();
                canInteract = false;
                _clickSound.Play();
            }
        }
        

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();

        if (interactable != null)
        {
            _interactable = interactable;
            _interactionCanvas.promptMessage =  _interactable.promptMessage;
        }

        InteractableRoom interactableRoom = collision.GetComponent<InteractableRoom>();

        if (interactableRoom != null )
        {
            _interactableRoom = interactableRoom;
            _interactionCanvasRoom.promptMessage = _interactableRoom.promptMessage;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_interactable != null)
            _interactable = null;
        else if (_interactableRoom != null)
            _interactableRoom = null;
    }


}
