using UnityEngine;

public static class GameConfig
{
    // SCENE
    public static string MENU_SCENE = "Menu";
    public static string GAME_SCENE = "Game";
    // RESOURCES
    // - SO
    // + MAP
    public static string CELL_PATH = "SO/CellDatas/";
    public static string EGG_PATH = "SO/EggDatas/";
    // - UI
    // + PANEL
    public static string PANEL_PATH = "UI/Panels/";

    // LINK
    public static string FACEBOOK_LINK = "https://www.facebook.com/ldcin2409";
    public static string GITHUB_LINK = "https://github.com/LDCin/Egg-Game";
    public static string PROPTIT_FACEBOOK_LINK = "https://www.facebook.com/clubproptit";

    // UI
    // - PANEL
    public static string PAUSE_PANEL = "Panel - PauseGame";
    public static string GAME_OVER_PANEL = "Panel - GameOver";
    public static string ACHIEVEMENTS_PANEL = "Panel - Achievements";
    public static string SETTING_PANEL = "Panel - Setting";
    public static string BONUS_PANEL = "Panel - Bonus";

    // SCORE
    public static int SCORE = 0;
    public static int MAX_EGG_LEVEL_IN_GAME = 1;
    public static int MAX_LEVEL_ON_START = 1;
    public static int MAX_EGG_LEVEL_HIGH_SCORE => PlayerPrefs.GetInt("MaxLevelHighScore", 1);

    // SOUND
    public static int BGM_STATE => PlayerPrefs.GetInt("BGMState", 1);
    public static int SFX_STATE => PlayerPrefs.GetInt("SFXState", 1);
}