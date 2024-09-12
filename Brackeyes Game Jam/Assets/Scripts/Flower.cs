using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private YoungerBrotherMovement _brother;

    [SerializeField] private Transform _restPos;
    [SerializeField] private Transform _torquePos;

    private Rigidbody2D rb;

    [SerializeField] private float _torque = 15;
    [SerializeField] private float _restoringForce = 300;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ApplyRestoringForce();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyTorque();
        } 

        else if (collision.gameObject.CompareTag("NPC"))
        {
            ApplyTorqueNPC();
        }
    }

    private void ApplyTorque()
    {
        rb.AddTorque(-_player.moveDir * _torque, ForceMode2D.Impulse);
    }

    private void ApplyTorqueNPC()
    {
        rb.AddTorque(-_brother.direction * _torque, ForceMode2D.Impulse);
    }

    private void ApplyRestoringForce()
    {
        rb.AddTorque(_restoringForce * (_torquePos.position.x - _restPos.position.x));
    }
}
