using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.say += Say;
    }

    private void Say() {
        Debug.Log("PROPTIT");
    }
}
