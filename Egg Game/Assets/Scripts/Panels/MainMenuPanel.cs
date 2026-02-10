using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanel : Panel
{
    [SerializeField] GameObject _helpPanel;
    // [SerializeField] GameObject _achievementsPanel;
    public void Start()
    {
        _helpPanel.SetActive(false);
        // _achievementsPanel.SetActive(false);
    }
    public void StartGame()
    {
        SoundManager.Instance.PlayClickSound();
        ParticleManager.Instance.PlayBubbleParticle();
        ParticleManager.Instance.StopCloudParticle();
        SceneManager.LoadScene(GameConfig.GAME_SCENE);
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
    }
    public void ShowAchievements()
    {
        SoundManager.Instance.PlayClickSound();
        PanelManager.Instance.OpenPanel(GameConfig.ACHIEVEMENTS_PANEL);
    }
    public void Setting()
    {
        SoundManager.Instance.PlayClickSound();
        PanelManager.Instance.OpenPanel(GameConfig.SETTING_PANEL);
    }
    public void Bonus()
    {
        SoundManager.Instance.PlayClickSound();
        PanelManager.Instance.OpenPanel(GameConfig.BONUS_PANEL);
    }
}
