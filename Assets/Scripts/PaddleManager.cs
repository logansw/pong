using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player1Prefab;
    [SerializeField] private PlayerController _player2Prefab;
    [SerializeField] private AIController _aiPrefab;
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

    public void Restart()
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

    public void OnStart()
    {
        Restart();
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

    public void SetPlayerCount(int count)
    {
        PlayerCount = count;
    }
}
