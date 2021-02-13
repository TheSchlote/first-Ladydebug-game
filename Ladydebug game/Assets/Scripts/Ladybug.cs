using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
    }

    public void MoveUp()
    {
        transform.position = Vector2.up;
    }
}

