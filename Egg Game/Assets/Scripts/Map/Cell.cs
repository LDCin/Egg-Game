using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Vector2 _posInBoard;
    [SerializeField] private Egg _egg;
    private Color _firstSpriteColor;
    [SerializeField] float _moveDistance = 0.009f;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _firstSpriteColor = _spriteRenderer.color;
    }
    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
    public float GetSize()
    {
        return _spriteRenderer.bounds.size.x;
    }
    public void SetSortingOrder(int sortingOrder)
    {
        _spriteRenderer.sortingOrder = sortingOrder;
    }
    public void SetPosInBoard(int x, int y)
    {
        _posInBoard = new Vector2(x, y);
    }
    public Vector2 GetPosInBoard()
    {
        return _posInBoard;
    }
    public Egg GetEgg()
    {
        return _egg;
    }
    public void SetEgg(Egg egg)
    {
        _egg = egg;
    }
    public void OnMouseDown()
    {
        GameManager.Instance.PickCell(this);
    }
    public void OnSelected()
    {
        _spriteRenderer.color = new Color(0.3f, 1f, 0.3f);
        transform.DOMoveY(transform.position.y + _moveDistance, 0.3f).SetEase(Ease.OutQuad);
    }
    public void OnUnselected()
    {
        _spriteRenderer.color = _firstSpriteColor;
        transform.DOMoveY(transform.position.y - _moveDistance, 0.3f).SetEase(Ease.OutQuad);
    }
    
}
