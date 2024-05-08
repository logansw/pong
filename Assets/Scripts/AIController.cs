using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Paddle controller for AI-controlled paddles.
/// </summary>
/// The AI will calculate where the ball is heading and move to intercept it.
/// The AI recalculates the result with a random degree of inaccuracy every so often to be less predictable and robotic.
/// When the ball is moving away from the paddle, it will move to the center of the screen.
public class AIController : PaddleController
{
    // Properties
    [SerializeField] private float _randomness; // Randomness factor to make the AI less predictable
    [SerializeField] private float _minReactionTime;    // Minimum time before the AI recalculates the target height
    [SerializeField] private float _maxReactionTime;    // Maximum time before the AI recalculates the target height

    // Private
    private Ball _ball;
    private Coroutine _moveCoroutine;   // Coroutine used to recalculate the target height
    private float _epsilon = 0.01f; // Margin of error for the AI to stop moving once it (almost) reaches the target height
    private float _targetHeight;    // The y-position the AI is aiming for

    void Start()
    {
        _ball = FindObjectOfType<Ball>();
    }

    void OnEnable()
    {
        _moveCoroutine = StartCoroutine(RecalculateTargetHeight());
    }

    void OnDisable()
    {
        StopCoroutine(_moveCoroutine);
    }

    /// <summary>
    /// Specifies how the paddle should move by returning the direction vector to move in.
    /// </summary>
    /// <returns>Direction to move in</returns>
    public override Vector3 Move()
    {
        return MoveTowardsTargetHeight(_targetHeight);
    }

    /// <summary>
    /// Checks if the ball is moving towards or away from the paddle.
    /// </summary>
    /// <returns>True if the ball is moving towards the paddle, false otherwise.</returns>
    private bool IsBallMovingTowardsPaddle()
    {
        Vector2 offset = _ball.transform.position - transform.position;
        Vector2 velocity = _ball.Velocity;
        return (offset.x < 0 && velocity.x < 0) || (offset.x > 0 && velocity.x > 0);
    }

    /// <summary>
    /// Moves the paddle towards the specified y-position.
    /// </summary>
    /// <param name="y">The target position to move towards</param>
    /// <returns>The direciton vector to move in</returns>
    private Vector3 MoveTowardsTargetHeight(float y)
    {
        if (transform.position.y < y - _epsilon)
        {
            return Vector3.up;
        }
        else if (transform.position.y > y + _epsilon)
        {
            return Vector3.down;
        }
        else
        {
            return Vector3.zero;
        }
    }

    /// <summary>
    /// Calculates the y-position the ball will be at when it reaches the AI's x-position.
    /// Doesn't take into account bounces off the top and bottom walls.
    /// Miscalculates the ball's trajectory by a random degree of inaccuracy.
    /// </summary>
    /// <returns>The predicted y-position of the ball when it reaches this x-position</returns>
    private float CalculateBallPosition()
    {
        Vector2 ballVelocity = _ball.Velocity;
        Vector2 ballPosition = _ball.transform.position;
        float m = ballVelocity.y / ballVelocity.x + Random.Range(-_randomness, _randomness);
        float b = ballPosition.y;
        float x = transform.position.x - ballPosition.x;
        return m * x + b;
    }

    /// <summary>
    /// Recalculates the target height the AI should aim for. After a delay, recalculates again.
    /// </summary>
    private IEnumerator RecalculateTargetHeight()
    {
        yield return new WaitForSeconds(Random.Range(_minReactionTime, _maxReactionTime));
        if (IsBallMovingTowardsPaddle())
        {
            _targetHeight = 0f;
        }
        else
        {
            _targetHeight = CalculateBallPosition();
        }
        _moveCoroutine = StartCoroutine(RecalculateTargetHeight());
    }
}