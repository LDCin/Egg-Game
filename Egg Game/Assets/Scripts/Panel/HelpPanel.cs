using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : Panel
{
    public void ReturnToGame()
    {
        Time.timeScale = 1;
        Close();
    }
}
