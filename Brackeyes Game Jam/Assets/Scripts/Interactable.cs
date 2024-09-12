using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage;

    public static event EventHandler ObjectInteract;

    public void BaseInteraction()
    {
        OnInteraction();
    }

    protected virtual void OnInteraction()
    {
        if (ObjectInteract != null)
        {
            ObjectInteract(this, EventArgs.Empty);
        }


    }


}
