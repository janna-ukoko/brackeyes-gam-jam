using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = GetMousePosition();
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        return mousePos;
    }
}
