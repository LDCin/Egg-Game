using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayPanel : Panel
{
    [SerializeField] private GameObject _helpPanel;
    [SerializeField] private GameObject _confirmBackToMenuPanel;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _maxEggLevelText;

    [SerializeField] private Image _time;
    [SerializeField] private float speedToEnd = 1f;
    public void Awake()
    {
        _confirmBackToMenuPanel.SetActive(false);
    }
    public void Update()
    {
        _time.fillAmount = Mathf.MoveTowards(_time.fillAmount, 0, Time.deltaTime * speedToEnd);
        if (_time.fillAmount == 0)
        {
            GameManager.Instance.GameOver();
        }
    }
    public void BackToMenu()
    {
        _confirmBackToMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ConfirmYes()
    {
        SceneManager.LoadScene(GameConfig.MENU_SCENE);
        PanelManager.Instance.CloseAllPanel();
    }
    public void ConfirmNo()
    {
        Time.timeScale = 1;
        _confirmBackToMenuPanel.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameConfig.GAME_SCENE);
    }
    public void Pause()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PAUSE_PANEL);
        Time.timeScale = 0;
    }
    public void Share()
    {
        
    }
    public void Help()
    {
        _helpPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResetTime()
    {
        _time.fillAmount = 1;
    }
    public void UpdateScoreText()
    {
        _scoreText.text = GameConfig.SCORE.ToString();
        ResetTime();
    }
    public void UpdateMaxEggLevelText()
    {
        _maxEggLevelText.text = GameConfig.MAX_EGG_LEVEL_IN_GAME.ToString();
    }
}
