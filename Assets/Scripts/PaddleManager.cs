using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Handles the creation of paddles based on the number of players.
/// </summary>
public class PaddleManager : Singleton<PaddleManager>
{
    // Prefabs
    [SerializeField] private PlayerController _player1Prefab;
    [SerializeField] private PlayerController _player2Prefab;
    [SerializeField] private AIController _aiPrefab;

    // Private
    private PaddleController _leftPaddle;
    private PaddleController _rightPaddle;
    private int PlayerCount;

    void Start()
    {
        PlayerCount = 0;
        OnStart();
    }

    void OnEnable()
    {
        GameManager.e_OnGameStart += OnStart;
    }

    /// <summary>
    /// Destroys old paddles
    /// </summary>
    public void CleanUp()
    {
        if (_leftPaddle != null)
        {
            Destroy(_leftPaddle.gameObject);
        }
        if (_rightPaddle != null)
        {
            Destroy(_rightPaddle.gameObject);
        }
    }

    /// <summary>
    /// To be called when the game starts. Creates and positions new paddles based on the number of players.
    /// </summary>
    public void OnStart()
    {
        CleanUp();
        switch (PlayerCount)
        {
            case 0:
                _leftPaddle = Instantiate(_aiPrefab);
                _rightPaddle = Instantiate(_aiPrefab);
                break;
            case 1:
                _leftPaddle = Instantiate(_player1Prefab);
                _rightPaddle = Instantiate(_aiPrefab);
                break;
            default:
                _leftPaddle = Instantiate(_player1Prefab);
                _rightPaddle = Instantiate(_player2Prefab);
                break;
        }
        _leftPaddle.transform.position = new Vector3(-10, 0, 0);
        _rightPaddle.transform.position = new Vector3(10, 0, 0);
    }

    /// <summary>
    /// Sets the number of players in the game.
    /// </summary>
    /// <param name="count">The number of players</param>
    /// TODO: Similar to GameManager, what if someone passes in a value other than 0, 1, or 2?
    /// This method totally allows it, but it's not clear what the expected behavior is.
    public void SetPlayerCount(int count)
    {
        PlayerCount = count;
    }
}
