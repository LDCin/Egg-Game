using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BonusPanel : Panel
{
    public void Gift()
    {
        SoundManager.Instance.PlayClickSound();
    }
    public void Arcade()
    {
        SoundManager.Instance.PlayClickSound();
    }
    public void Flash()
    {
        SoundManager.Instance.PlayClickSound();
    }
    public void Back()
    {
        SoundManager.Instance.PlayClickSound();
        Close();
    }
}
