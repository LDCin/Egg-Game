using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    [SerializeField] private bool _destroyOnReturn = false;
    private int _id;
    private int _level;
    private SpriteRenderer _spriteRender;
    private Animator _animator;
    private void Awake()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    public void SetUp(int id, Sprite sprite, RuntimeAnimatorController eggAnimation)
    {
        _id = id;
        _level = id;
        _spriteRender.sprite = sprite;
        _animator.runtimeAnimatorController = eggAnimation;
    }
    public int GetID()
    {
        return _id;
    }
    public float GetSize()
    {
        return _spriteRender.bounds.size.x;
    }
    public int GetLevel()
    {
        return _level;
    }
    public void ReturnToPool()
    {
        transform.SetParent(null);
        if (_destroyOnReturn) Destroy(gameObject);
        else
        {
            gameObject.SetActive(false);
        }
    }
    
}
