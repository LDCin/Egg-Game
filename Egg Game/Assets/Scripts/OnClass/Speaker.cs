using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    public void OnClick()
    {
        EventManager.say?.Invoke();
    }
}
