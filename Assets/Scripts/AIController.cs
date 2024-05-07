using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PaddleController
{
    private Ball _ball;
    private Coroutine _moveCoroutine;
    [SerializeField] private float _randomness;
    [SerializeField] private float _minReactionTime;
    [SerializeField] private float _maxReactionTime;
    private float _epsilon = 0.01f;
    private float _targetHeight;

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

    public override Vector3 Move()
    {
        return MoveTowardsTargetHeight(_targetHeight);
    }

    private bool IsBallMovingTowardsPaddle()
    {
        Vector2 offset = _ball.transform.position - transform.position;
        Vector2 velocity = _ball.GetVelocity();
        return Vector2.Dot(offset, velocity) > 0;
    }

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

    private float CalculateBallPosition()
    {
        Vector2 ballVelocity = _ball.GetVelocity();
        Vector2 ballPosition = _ball.transform.position;
        float m = ballVelocity.y / ballVelocity.x + Random.Range(-_randomness, _randomness);
        float b = ballPosition.y;
        float x = transform.position.x - ballPosition.x;
        return m * x + b;
    }

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