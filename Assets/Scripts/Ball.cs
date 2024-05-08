using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    // Properties
    [SerializeField] private float _initialSpeed;
    [SerializeField] private float _speedIncrease = 1f;
    private float _speed;
    private Rigidbody2D _rigidbody2D;
    private float _sharpestLaunchAngle = 45f;
    private float _height;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _height = transform.localScale.y;
    }

    void OnEnable()
    {
        GameManager.e_OnGameStart += Restart;
    }

    void OnDisable()
    {
        GameManager.e_OnGameStart -= Restart;
    }

    void Start()
    {
        Restart();
    }

    // Spawns the ball at the center of the map and sends it in a random direction
    public void Restart()
    {
        _speed = _initialSpeed;
        transform.position = Vector3.zero;
        _rigidbody2D.velocity = GetRandomStartDirection(_sharpestLaunchAngle) * _speed;
    }

    void Update()
    {
        if (GameManager.s_Instance.GameState.Equals(GameState.GameOver)) { return; }

        CheckVerticalOutOfBounds();
        CheckHorizontalOutOfBounds();
    }

    private void CheckVerticalOutOfBounds()
    {
        if ((transform.position.y + (_height / 2)) > Camera.main.orthographicSize)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -Mathf.Abs(_rigidbody2D.velocity.y));
            PlayBounceSound();
        }
        else if ((transform.position.y - (_height / 2f)) < -Camera.main.orthographicSize)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Mathf.Abs(_rigidbody2D.velocity.y));
            PlayBounceSound();
        }
    }

    private void CheckHorizontalOutOfBounds()
    {
        if ((transform.position.x + (_height / 2)) > Camera.main.orthographicSize * Camera.main.aspect)
        {
            GameManager.s_Instance.IncrementScore(1);
            Restart();
        }
        else if ((transform.position.x - (_height / 2)) < -Camera.main.orthographicSize * Camera.main.aspect)
        {
            GameManager.s_Instance.IncrementScore(2);
            Restart();
        }
    }

    private Vector3 GetRandomStartDirection(float angleRange)
    {
        float y = Mathf.Tan(angleRange);
        Vector3 dir = new Vector3(1.0f, Random.Range(y, -y), 0.0f).normalized;
        if (Random.Range(0, 2) == 1)
        {
            dir.x *= -1;
        }
        return dir;
    }

    public void SetDirection(Vector3 dir)
    {
        _rigidbody2D.velocity = dir * _speed;
    }

    public Vector2 GetVelocity()
    {
        return _rigidbody2D.velocity;
    }

    private void PlayBounceSound()
    {
        AudioManager.s_Instance.LowBeepSmooth.Play();
    }

    public void IncreaseSpeed()
    {
        _speed += _speedIncrease;
    }
}