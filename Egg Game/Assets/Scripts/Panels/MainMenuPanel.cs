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
        SceneManager.LoadScene(GameConfig.GAME_SCENE);
    }
    public void Share()
    {
        Application.OpenURL(GameConfig.GITHUB_LINK);
    }
    public void Help()
    {
        _helpPanel.SetActive(true);
    }
    public void ShowAchievements()
    {
        PanelManager.Instance.OpenPanel(GameConfig.ACHIEVEMENTS_PANEL);
    }
    public void Setting()
    {
        PanelManager.Instance.OpenPanel(GameConfig.SETTING_PANEL);
    }
    public void Bonus()
    {
        PanelManager.Instance.OpenPanel(GameConfig.BONUS_PANEL);
    }
}
