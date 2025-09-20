using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    private int _id;
    private SpriteRenderer _spriteRender;
    private Animator _animator;
    public void SetUp(int id, Sprite sprite, RuntimeAnimatorController eggAnimation)
    {
        _id = id;
        _spriteRender.sprite = sprite;
        _animator.runtimeAnimatorController = eggAnimation;
    }
}
