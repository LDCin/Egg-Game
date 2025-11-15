using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _BGM;
    [SerializeField] private AudioSource _SFX;

    [SerializeField] private AudioClip _defaultSFX;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _gameOverSound;

    public override void Awake()
    {
        base.Awake();
        if (GameConfig.BGM_STATE == 1) PlayBGM();
        else StopBGM();
        if (GameConfig.SFX_STATE == 1) PlaySFX();
        else PlaySFX();
    }
    public void PlayBGM()
    {
        if (_BGM == null)
        {
            Debug.Log("Not Found: BGM");
        }
        _BGM.loop = true;
        _BGM.mute = false;
        PlayerPrefs.SetInt("BGMState", 1);
        PlayerPrefs.Save();
    }
    public void StopBGM()
    {
        _BGM.mute = true;
        PlayerPrefs.SetInt("BGMState", 0);
        PlayerPrefs.Save();
    }
    public void PlayClickSound()
    {
        if (_clickSound == null)
        {
            Debug.Log("Not Found: Click Sound");
        }
        _SFX.PlayOneShot(_clickSound);
    }
    public void StopSFX()
    {
        _SFX.mute = true;
        PlayerPrefs.SetInt("SFXState", 0);
        PlayerPrefs.Save();
    }
    public void PlaySFX()
    {
        _SFX.mute = false;
        PlayerPrefs.SetInt("SFXState", 1);
        PlayerPrefs.Save();
    }
    public void PlayGameOverSound()
    {
        _SFX.PlayOneShot(_gameOverSound);
    }
    public void ChangeSFXState()
    {
        if (GameConfig.SFX_STATE == 1) StopSFX();
        else PlaySFX();
    }
    public void ChangeBGMState()
    {
        if (GameConfig.BGM_STATE == 1) StopBGM();
        else PlayBGM();
    }
}