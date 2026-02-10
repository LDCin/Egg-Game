using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private Image _currentLevelEgg;
    [SerializeField] private Image _nextLevelEgg;
    private bool _isOpenGameOverPanel = false;
    private void Awake()
    {
        _confirmBackToMenuPanel.SetActive(false);
        _currentLevelEgg.preserveAspect = true;
        _nextLevelEgg.preserveAspect = true;
    }
    private void Start()
    {
        UpdateEggLevelImage();
    }
    private void Update()
    {
        _time.fillAmount = Mathf.MoveTowards(_time.fillAmount, 0, Time.deltaTime * speedToEnd);
        if (_time.fillAmount == 0 && !_isOpenGameOverPanel)
        {
            GameManager.Instance.GameOver();
            _isOpenGameOverPanel = true;
        }
    }
    public void BackToMenu()
    {
        SoundManager.Instance.PlayClickSound();
        _confirmBackToMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ConfirmYes()
    {
        SoundManager.Instance.PlayClickSound();
        ParticleManager.Instance.PlayBubbleParticle();
        ParticleManager.Instance.StopCloudParticle();
        Time.timeScale = 1;
        SceneManager.LoadScene(GameConfig.MENU_SCENE);
        PanelManager.Instance.CloseAllPanel();
    }
    public void ConfirmNo()
    {
        SoundManager.Instance.PlayClickSound();
        Time.timeScale = 1;
        _confirmBackToMenuPanel.SetActive(false);
    }
    public void Restart()
    {
        SoundManager.Instance.PlayClickSound();
        Time.timeScale = 1;
        SceneManager.LoadScene(GameConfig.GAME_SCENE);
    }
    public void Pause()
    {
        SoundManager.Instance.PlayClickSound();
        PanelManager.Instance.OpenPanel(GameConfig.PAUSE_PANEL);
        Time.timeScale = 0;
    }
    public void Share()
    {
        SoundManager.Instance.PlayClickSound();
        Application.OpenURL(GameConfig.GITHUB_LINK);
    }
    public void Help()
    {
        SoundManager.Instance.PlayClickSound();
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
        _maxEggLevelText.text = (GameConfig.MAX_EGG_LEVEL_IN_GAME - 1).ToString();
        UpdateEggLevelImage();
    }
    private void UpdateEggLevelImage()
    {
        _currentLevelEgg.sprite = Resources.Load<EggData>(GameConfig.EGG_PATH + "Egg " + (GameConfig.MAX_EGG_LEVEL_IN_GAME - 1)).sprite;
        _nextLevelEgg.sprite = Resources.Load<EggData>(GameConfig.EGG_PATH + "Egg " + (GameConfig.MAX_EGG_LEVEL_IN_GAME)).sprite;
    }
}
