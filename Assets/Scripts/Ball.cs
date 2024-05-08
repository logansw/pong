using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ball blob component that handles the ball's movement and collision.
/// </summary>
/// Ball bounces off the top and bottom walls, and scores a point when it hits the left or right walls.
/// The ball's speed increases every time it bounces off a paddle.
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    // Component References
    private Rigidbody2D _rigidbody2D;

    // Properties
    [SerializeField] private float _initialSpeed;
    [SerializeField] private float _speedIncrease = 1f;

    // Public
    public Vector2 Velocity => _rigidbody2D.velocity;


    // Private
    private float _sharpestLaunchAngle = 45f;   // The maximum angle off of horizontal the ball can launch at
    private float _speed;   // Current speed of the ball
    private float _height;  // Height of the ball sprite

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

    void Update()
    {
        if (GameManager.s_Instance.GameState.Equals(GameState.GameOver)) { return; }

        CheckVerticalOutOfBounds();
        CheckHorizontalOutOfBounds();
    }

    /// <summary>
    /// Spawns the ball at the center of the map and sends it in a random direction.
    /// Also resets speed of the ball to its initial speed
    /// </summary>
    public void Restart()
    {
        _speed = _initialSpeed;
        transform.position = Vector3.zero;
        _rigidbody2D.velocity = GetRandomStartDirection(_sharpestLaunchAngle) * _speed;
    }

    /// <summary>
    /// Checks if the ball is out of bounds vertically and bounces it back in the opposite direction.
    /// </summary>
    /// TODO: Maybe this should be refactored to be more generic and reusable. I.e. return a bool indicating if the
    /// ball was out of bounds, or a Vector indicating the direction it was out of bounds in.
    /// TODO: Maybe there should be an event that triggers the bounce sound so that we don't have to mix in audio in
    /// this script.
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

    /// <summary>
    /// Checks if the ball is out of bounds horizontally and increments the score of the player who scored.
    /// </summary>
    /// TODO: Same as above, maybe this should be refactored to be more generic and reusable.
    /// TODO: Should probably trigger an event to tell the GameManager to increment the score instead of calling
    /// it directly. Ball shouldn't really be handling this logic, makes more sense if its in GameManager.
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

    /// <summary>
    /// Returns a random direction vector within the specified angle range.
    /// </summary>
    /// <param name="angleRange">Angle above and below horizontal that the ball can be launched between</param>
    /// <returns></returns>
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

    /// <summary>
    /// Set the ball's velocity to the specified direction
    /// </summary>
    /// <param name="dir">The direction to change the ball's velocity direction to</param>
    public void SetDirection(Vector3 dir)
    {
        _rigidbody2D.velocity = dir * _speed;
    }

    private void PlayBounceSound()
    {
        AudioManager.s_Instance.LowBeepSmooth.Play();
    }

    /// <summary>
    /// Increases the speed of the ball by the specified amount
    /// </summary>
    public void IncreaseSpeed()
    {
        _speed += _speedIncrease;
    }
}