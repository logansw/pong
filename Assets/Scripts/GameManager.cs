using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Handles game flow and state, and tracks the score
public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _p1ScoreText;
    [SerializeField] private TMP_Text _p2ScoreText;
    public static GameManager s_instance;
    private int _leftScore;
    private int _rightScore;

    void Awake()
    {
        if (s_instance != null)
        {
            Destroy(s_instance.gameObject);
        }
        s_instance = this;
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
        RenderScores();
    }

    public void ResetScores()
    {
        _leftScore = 0;
        _rightScore = 0;
        RenderScores();
    }

    private void RenderScores()
    {
        _p1ScoreText.text = _leftScore.ToString();
        _p2ScoreText.text = _rightScore.ToString();
    }
}