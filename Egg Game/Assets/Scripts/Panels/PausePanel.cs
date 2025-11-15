using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : Panel
{
    [SerializeField] private Sprite _onSFXSprite;
    [SerializeField] private Sprite _offSFXSprite;
    [SerializeField] private Sprite _onBGMSprite;
    [SerializeField] private Sprite _offBGMSprite;
    [SerializeField] private Button _SFXButton;
    [SerializeField] private Button _BGMButton;
    public void Start()
    {
        ChangeBGMSprite();
        ChangeSFXSprite();
    }
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
        SoundManager.Instance.ChangeSFXState();
        ChangeSFXSprite();
    }
    public void ChangeSFXSprite()
    {
        if (GameConfig.SFX_STATE == 1)
        {
            _SFXButton.image.sprite = _onSFXSprite;
        }
        else
        {
            _SFXButton.image.sprite = _offSFXSprite;

        }
    }
    public void ChangeBGMState()
    {
        SoundManager.Instance.ChangeBGMState();
        ChangeBGMSprite();
    }
    public void ChangeBGMSprite()
    {
        if (GameConfig.BGM_STATE == 1)
        {
            _BGMButton.image.sprite = _onBGMSprite;
        }
        else
        {
            _BGMButton.image.sprite = _offBGMSprite;

        }
    }
    public void OpenNoAdsPage()
    {

    }
}
