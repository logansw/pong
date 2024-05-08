using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PaddleController))]
public class Paddle : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
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
        AudioManager.s_Instance.LowBeepSmooth.Play();
    }

    // Prevents the paddle from moving off screen
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