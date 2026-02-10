using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Egg : MonoBehaviour
{
    [SerializeField] private bool _destroyOnReturn = false;
    [SerializeField] private int _id;
    [SerializeField] private int _level;
    [SerializeField] private GameObject _shadow;

    // [SerializeField] private int _minTimeToJump = 2;
    // [SerializeField] private int _maxTimeToJump = 6;
    // [SerializeField] private float _timeToJump;
    private SpriteRenderer _spriteRender = null;
    private Animator _animator = null;
    private Image _image;
    private void Awake()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            _spriteRender = GetComponent<SpriteRenderer>();
        }
        if (GetComponent<Animator>() != null)
        {
            _animator = GetComponent<Animator>();
        }
        if (GetComponent<Image>() != null)
        {
            _image = GetComponent<Image>();
        }
    }
    // private void Start()
    // {
    //     _timeToJump = Random.Range(_minTimeToJump, _maxTimeToJump);
    // }
    // private void Update()
    // {
    //     _timeToJump -= Time.deltaTime;
    //     if (_timeToJump <= 0)
    //     {
    //         _animator.SetTrigger(GameConfig.JUMP_TRIGGER);
    //         _timeToJump = Random.Range(_minTimeToJump, _maxTimeToJump);
    //     }
    // }

    public void SetUp(int id, Sprite sprite, RuntimeAnimatorController eggAnimation)
    {
        _id = id;
        _level = id + 1;
        if (_spriteRender != null)
        {
            _spriteRender.sprite = sprite;
        }
        if (_animator != null)
        {
            _animator.runtimeAnimatorController = eggAnimation;
        }
        if (_level > 1 && !(_shadow.CompareTag(GameConfig.SHADOW_TAG))) {
            _shadow.gameObject.SetActive(true);
        }
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
    public void SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
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
