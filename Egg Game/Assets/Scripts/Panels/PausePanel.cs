using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : Panel
{
    public void OpenFacebookLink()
    {
        Application.OpenURL(GameConfig.FACEBOOK_LINK);
    }
    public void OpenRatePage()
    {
        Application.OpenURL(GameConfig.GITHUB_LINK);
    }
    public void OpenInforPage()
    {
        Application.OpenURL(GameConfig.PROPTIT_FACEBOOK_LINK);
    }
    public void ReturnToGame()
    {
        Time.timeScale = 1;
        Close();
    }
    public void ChangeSFXState()
    {

    }
    public void ChangeBGMState()
    {

    }
    public void OpenNoAdsPage()
    {
        
    }
}
