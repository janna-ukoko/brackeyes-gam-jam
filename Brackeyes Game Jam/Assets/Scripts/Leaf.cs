using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private PlayerMovement _player;
    [SerializeField] private YoungerBrotherMovement _brother;

    [SerializeField] private float _force = 5f;
    [SerializeField] private float _torque = 5f;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BoxCollider2D>().tag == "PlayerTrigger")
        {
            _rb.AddForce(new Vector2(_player.moveDir, 1f) * _force, ForceMode2D.Impulse);
            _rb.AddTorque(_player.moveDir * -_torque, ForceMode2D.Impulse);
        }

        else if (collision.GetComponent<BoxCollider2D>().tag == "NPCTrigger")
        {
            _rb.AddForce(new Vector2(_brother.direction, 1f) * _force, ForceMode2D.Impulse);
            _rb.AddTorque(_brother.direction * -_torque, ForceMode2D.Impulse);
        }
    }

}
