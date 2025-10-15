using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private bool _destroyOnReturn = false;
    [SerializeField] private int _id;
    [SerializeField] private int _level;
    private SpriteRenderer _spriteRender;
    private Animator _animator;
    private void Awake()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // private void OnEnable()
    // {
    //     Jump();
    // }

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

    public void LevelUp(EggPool eggPool)
    {
        Egg nextLevelEgg = eggPool.GetEgg(_id + 1);
        nextLevelEgg.transform.position = transform.position;
        nextLevelEgg.transform.SetParent(transform.parent);
        nextLevelEgg.gameObject.SetActive(true);
        Cell cell = transform.parent.GetComponent<Cell>();
        cell.SetEgg(nextLevelEgg);
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        if (transform.parent != null)
        {
            Cell cell = transform.parent.GetComponent<Cell>();
            if (cell.GetEgg() == this)
            {
                cell.SetEgg(null);
            }
            transform.SetParent(null);
        }
        if (_destroyOnReturn) Destroy(gameObject);
        else
        {
            gameObject.SetActive(false);
        }
    }

}
