using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : Panel
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private List<GameObject> _starList;
    [SerializeField] private Image _eggLevelScore;
    public void OnEnable()
    {
        UpdateScoreText();
        UpdateHighScoreText();
        UpdateMaxEggLevelScore();
        Time.timeScale = 0;
        ShowStar();
    }
    private void UpdateScoreText()
    {
        _scoreText.text = GameConfig.SCORE.ToString();
    }
    private void UpdateHighScoreText()
    {
        _highScoreText.text = GameConfig.HIGH_SCORE.ToString();
    }
    private void UpdateMaxEggLevelScore()
    {
        _eggLevelScore.sprite = Resources.Load<EggData>(GameConfig.EGG_PATH + "Egg " + (GameConfig.MAX_EGG_LEVEL_IN_GAME - 1)).sprite;
        _eggLevelScore.preserveAspect = true;
    }
    public void BackToMenu()
    {
        SoundManager.Instance.PlayClickSound();
        Close();
        SceneManager.LoadScene(GameConfig.MENU_SCENE);
    }
    public void Restart()
    {
        SoundManager.Instance.PlayClickSound();
        Time.timeScale = 1;
        SceneManager.LoadScene(GameConfig.GAME_SCENE);
    }
    public void ShowStar()
    {
        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);

        float animTime = 0.8f;

        for (int i = 0; i < GameConfig.STAR_SCORE; i++)
        {
            int index = i;

            seq.AppendCallback(() =>
            {
                _starList[index].SetActive(true);
            });

            seq.AppendInterval(animTime);
        }
    }
}
