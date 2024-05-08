using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Handles game flow and state, and tracks the score.
/// </summary>
/// TODO: More time could be spent building out a nicer state machine for the game that
/// provides a single place to manage the game state and transitions, ensuring that all
/// necessary changes are made when transitioning between states, in a way that avoids
/// the unpredictable execution order that happens when multiple functions are triggered
/// by an event. A little overkill for this project perhaps, but want to recognize the room
/// for improvmement if working on a larger project.
public class GameManager : Singleton<GameManager>
{
    // Public
    public static Action e_OnGameStart;
    public GameState GameState;

    // External References
    [SerializeField] private UIManager _uiManager;

    // Private
    private int _leftScore;
    private int _rightScore;

    protected override void Awake()
    {
        base.Awake();
        GameState = GameState.PreStart;
    }

    /// <summary>
    /// Increments the score of the given player.
    /// </summary>
    /// <param name="player">The player's ID. 1 for player 1, and 2 for player 2.</param>
    /// TODO: What if someone passes in a value other than 1 or 2? How can we restructure to prevent this?
    /// A bool feels a little unintuitive IMO. Maybe an enum, but it feels kind of funny to have such an enum.
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

    /// <summary>
    /// Resets scores of both players to 0
    /// </summary>
    public void ResetScores()
    {
        _leftScore = 0;
        _rightScore = 0;
        _uiManager.RenderScores(_leftScore, _rightScore);
    }

    /// <summary>
    /// Starts the game and resets the scores.
    /// </summary>
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