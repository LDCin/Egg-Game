using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private int s;
    [ContextMenu("Test DOMove")]
    public void Move()
    {
        Debug.Log("Hehe");
        transform.DOMove(transform.position + Vector3.left * s, 2f).SetEase(Ease.Linear).OnStepComplete(() =>
        {
            transform.DOMove(transform.position + Vector3.down * s, 2f).SetLoops(3);
        });
        // tween = transform.DO...
        // easing
        // OnComplete
    }
    [ContextMenu("Test DOScale")]
    public void ShowBoard()
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).From(new Vector3(0, 0, 0)).SetEase(Ease.OutBounce).SetDelay(0.5f);
        // transform.DOScale(new Vector3(1f, 1f, 1f), 3f).From(new Vector3(0, 0, 0)).SetEase(Ease.OutBack);
    }
}
