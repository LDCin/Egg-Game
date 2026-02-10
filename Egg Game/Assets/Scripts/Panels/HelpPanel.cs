using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : Panel
{
    public void ReturnToGame()
    {
        SoundManager.Instance.PlayClickSound();
        Time.timeScale = 1;
        Close();
    }
}
