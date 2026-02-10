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
        SoundManager.Instance.PlayClickSound();
        Application.OpenURL(GameConfig.FACEBOOK_LINK);
    }
    public void OpenRatePage()
    {
        SoundManager.Instance.PlayClickSound();
        Application.OpenURL(GameConfig.GITHUB_LINK);
    }
    public void OpenInforPage()
    {
        SoundManager.Instance.PlayClickSound();
        Application.OpenURL(GameConfig.PROPTIT_FACEBOOK_LINK);
    }
    public void ReturnToGame()
    {
        SoundManager.Instance.PlayClickSound();
        Time.timeScale = 1;
        Close();
    }
    public void ChangeSFXState()
    {
        SoundManager.Instance.PlayClickSound();
        SoundManager.Instance.ChangeSFXState();
        ChangeSFXSprite();
    }
    public void ChangeSFXSprite()
    {
        SoundManager.Instance.PlayClickSound();
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
        SoundManager.Instance.PlayClickSound();
        SoundManager.Instance.ChangeBGMState();
        ChangeBGMSprite();
    }
    public void ChangeBGMSprite()
    {
        SoundManager.Instance.PlayClickSound();
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
        SoundManager.Instance.PlayClickSound();
    }
}
