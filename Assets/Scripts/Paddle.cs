using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Blob component that handles the paddle's movement and collision. Requires a PaddleController component
/// to determine what logic the paddle should use to determine how to move (e.g. Player-controlled vs. AI-controlled).
/// </summary>
[RequireComponent(typeof(PaddleController))]
public class Paddle : MonoBehaviour
{
    // Properties
    [SerializeField] private float _speed = 5f;
    // Private
    private PaddleController _paddleController;
    private float _height;

    void Awake()
    {
        _paddleController = GetComponent<PaddleController>();
        _height = transform.localScale.y;
    }

    void Update()
    {
        transform.position += _paddleController.Move() * _speed * Time.deltaTime;
        ClampPositionToCamera();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Ball ball = other.gameObject.GetComponent<Ball>();
        Vector2 offset = new Vector2(other.transform.position.x - transform.position.x, (other.transform.position.y - transform.position.y) / 2f);
        ball.SetDirection(offset.normalized);
        ball.IncreaseSpeed();
        AudioManager.s_Instance.LowBeepSmooth.Play();
    }

    /// <summary>
    /// Prevents the paddle from moving off-screen
    /// </summary>
    private void ClampPositionToCamera()
    {
        if (transform.position.y + _height / 2 > Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, Camera.main.orthographicSize - _height / 2, transform.position.z);
        }
        else if (transform.position.y - _height / 2 < -Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, -Camera.main.orthographicSize + _height / 2, transform.position.z);
        }
    }
}