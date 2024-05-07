using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PaddleController
{
    [SerializeField] private KeyCode _keyUp;
    [SerializeField] private KeyCode _keyDown;
    public override Vector3 Move()
    {
        if (Input.GetKey(_keyUp))
        {
            return Vector3.up;
        }
        else if (Input.GetKey(_keyDown))
        {
            return Vector3.down;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
