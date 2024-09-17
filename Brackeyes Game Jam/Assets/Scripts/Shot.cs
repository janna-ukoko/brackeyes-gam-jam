using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Rigidbody2D _rb;

    public Vector2 dir;

    [SerializeField] private float _speed = 10f;

    private float _destroyTimer = 0.2f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = dir * _speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject != null) StartCoroutine(DestroyShot());
    }

    private IEnumerator DestroyShot()
    {
        _rb.velocity = new Vector2();

        yield return new WaitForSeconds(_destroyTimer);

        Destroy(gameObject);
    }
}
