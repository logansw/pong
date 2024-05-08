using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Handles game flow and state, and tracks the score
public class GameManager : Singleton<GameManager>
{
    private int _leftScore;
    private int _rightScore;
    public GameState GameState;
    public static Action e_OnGameStart;
    [SerializeField] private UIManager _uiManager;

    protected override void Awake()
    {
        base.Awake();
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

        if (!GameState.Equals(GameState.PreStart) && _leftScore >= 5 || _rightScore >= 5)
        {
            GameState = GameState.GameOver;
            _uiManager.ShowGameOverPanel(_leftScore > _rightScore ? 1 : 2);
            AudioManager.s_Instance.HighBeepSmooth.Play();
        }
        AudioManager.s_Instance.Score.Play();
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