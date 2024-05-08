using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles all UI elements.
/// </summary>
public class UIManager : Singleton<UIManager>
{
    // External References
    [SerializeField] private TMP_Text _p1ScoreText;
    [SerializeField] private TMP_Text _p2ScoreText;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _winnerText;

    /// <summary>
    /// Updates the score text as specified.
    /// </summary>
    /// <param name="leftScore">Player 1's score</param>
    /// <param name="rightScore">Player 2's score</param>
    public void RenderScores(int leftScore, int rightScore)
    {
        _p1ScoreText.text = leftScore.ToString();
        _p2ScoreText.text = rightScore.ToString();
    }

    /// <summary>
    /// Shows the start panel and hides the game over panel.
    /// </summary>
    public void ShowStartPanel()
    {
        _startPanel.SetActive(true);
        _gameOverPanel.SetActive(false);
    }

    /// <summary>
    /// Shows the game over panel.
    /// </summary>
    public void ShowGameOverPanel(int winner)
    {
        _winnerText.text = "Player " + winner + " wins!";
        _gameOverPanel.SetActive(true);
    }

    /// <summary>
    /// Hides all panels.
    /// </summary>
    public void HidePanels()
    {
        _startPanel.SetActive(false);
        _gameOverPanel.SetActive(false);
    }
}
