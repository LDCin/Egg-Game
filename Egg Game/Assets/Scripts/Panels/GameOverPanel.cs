using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : Panel
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private List<GameObject> _starList;
    public void OnEnable()
    {
        UpdateScoreText();
        UpdateHighScoreText();
        Time.timeScale = 0;
        ShowStar();
    }
    public void UpdateScoreText()
    {
        _scoreText.text = GameConfig.SCORE.ToString();
    }
    public void UpdateHighScoreText()
    {
        _highScoreText.text = GameConfig.HIGH_SCORE.ToString();
    }
    public void BackToMenu()
    {
        Close();
        SceneManager.LoadScene(GameConfig.MENU_SCENE);
    }
    public void Restart()
    {
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
