using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Paddle controller for player-controlled paddles. Set the up and down keys in the inspector.
/// </summary>
public class PlayerController : PaddleController
{
    // Properties
    [SerializeField] private KeyCode _keyUp;
    [SerializeField] private KeyCode _keyDown;

    /// <summary>
    /// Moves the paddle based on the player's input.
    /// </summary>
    /// <returns>The direction to move in</returns>
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
