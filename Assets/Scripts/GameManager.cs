using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Handles game flow and state, and tracks the score
public class GameManager : MonoBehaviour
{
    public static GameManager s_instance;
    private int _leftScore;
    private int _rightScore;
    public GameState GameState;
    public static Action e_OnGameStart;
    [SerializeField] private UIManager _uiManager;

    void Awake()
    {
        if (s_instance != null)
        {
            Destroy(s_instance.gameObject);
        }
        s_instance = this;
        GameState = GameState.PreStart;
    }

    public void IncrementScore(int player)
    {
        if (player == 1)
        {
            _leftScore++;
        }
        else if (player == 2)
        {
            _rightScore++;
        }
        _uiManager.RenderScores(_leftScore, _rightScore);
    }

    public void ResetScores()
    {
        _leftScore = 0;
        _rightScore = 0;
        _uiManager.RenderScores(_leftScore, _rightScore);
    }

    public void StartGame()
    {
        GameState = GameState.Playing;
        ResetScores();
        e_OnGameStart?.Invoke();
        _uiManager.HidePanels();
    }
}

public enum GameState
{
    PreStart,
    Playing,
    GameOver
}