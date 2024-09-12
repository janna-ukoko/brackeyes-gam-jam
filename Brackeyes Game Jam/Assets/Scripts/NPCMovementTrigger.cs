using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementTrigger : MonoBehaviour
{
    [SerializeField] private YoungerBrotherMovement _youngerBrother;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerTrigger")
        {
            StartCoroutine(_youngerBrother.BeginMovement());
        }
    }
}
