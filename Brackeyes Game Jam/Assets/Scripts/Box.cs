using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;

    [SerializeField] private CameraShake _shake;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            _shake.Shake();

            InstantiateExplosion();

            Destroy(gameObject);
        }
    }

    private void InstantiateExplosion()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }
}
