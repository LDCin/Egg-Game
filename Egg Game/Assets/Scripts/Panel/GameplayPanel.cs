using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayPanel : Panel
{
    [SerializeField] GameObject _helpPanel;
    [SerializeField] GameObject _confirmBackToMenuPanel;
    public void Awake()
    {
        _confirmBackToMenuPanel.SetActive(false);
    }
    public void BackToMenu()
    {
        _confirmBackToMenuPanel.SetActive(true);
    }
    public void ConfirmYes()
    {
        PanelManager.Instance.CloseAllPanel();
        SceneManager.LoadScene(GameConfig.MENU_SCENE);
    }
    public void ConfirmNo()
    {
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
}
