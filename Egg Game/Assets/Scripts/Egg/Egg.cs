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

    // ANIMATIONs
    [SerializeField] float _minDelayTime = 3f;
    [SerializeField] float _maxDelayTime = 10f;
    [SerializeField] float _upTime = 0.5f;
    [SerializeField] float _downTime = 0.4f;
    [SerializeField] float _preSquashTime = 0.12f;
    [SerializeField] float _impactTime = 0.10f;

    [SerializeField] float _jumpHeight = 0.5f;

    [SerializeField, Range(0f, 0.5f)] float _squashRate = 0.18f;
    [SerializeField, Range(0f, 0.5f)] float _stretchRate = 0.22f;
    [SerializeField, Range(0f, 0.5f)] float _impactSquashRate = 0.15f;
    

    private void OnEnable()
    {
        Jump();
    }

    void Jump()
    {
        Sequence _seq = DOTween.Sequence();
        var t = transform;
        Vector3 startPos = t.position;
        Vector3 startScale = t.localScale;
        
        Vector3 Squash(float rate) => new Vector3(startScale.x * (1f + rate), startScale.y * (1f - rate), startScale.z);
        Vector3 Stretch(float rate) => new Vector3(startScale.x * (1f - rate), startScale.y * (1f + rate), startScale.z);

        _seq.AppendInterval(Random.Range(_minDelayTime, _maxDelayTime));

        _seq.Append(t.DOScale(Squash(_squashRate), _preSquashTime).SetEase(Ease.OutQuad));
        _seq.Append(t.DOMoveY(startPos.y + _jumpHeight, _upTime).SetEase(Ease.OutQuad));
        _seq.Join(t.DOScale(Stretch(_stretchRate), _upTime * 0.6f).SetEase(Ease.OutQuad));
        _seq.Join(t.DOScale(startScale, _upTime * 0.6f).SetEase(Ease.InSine).SetDelay(_upTime * 0.4f));
        _seq.Append(t.DOMoveY(startPos.y, _downTime).SetEase(Ease.InQuad));
        // _seq.Join(t.DOScale(Squash(_impactSquashRate), _impactTime).SetEase(Ease.OutQuad));
        // _seq.Append(t.DOScale(startScale, _impactTime * 1.2f).SetEase(Ease.OutBack, 1.2f));

        _seq.SetLoops(-1, LoopType.Restart);
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
