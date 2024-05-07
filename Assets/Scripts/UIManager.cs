using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _p1ScoreText;
    [SerializeField] private TMP_Text _p2ScoreText;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _gameOverPanel;

    public void RenderScores(int leftScore, int rightScore)
    {
        _p1ScoreText.text = leftScore.ToString();
        _p2ScoreText.text = rightScore.ToString();
    }

    public void ShowStartPanel()
    {
        _startPanel.SetActive(true);
        _gameOverPanel.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        _startPanel.SetActive(false);
        _gameOverPanel.SetActive(true);
    }

    public void HidePanels()
    {
        _startPanel.SetActive(false);
        _gameOverPanel.SetActive(false);
    }
}
