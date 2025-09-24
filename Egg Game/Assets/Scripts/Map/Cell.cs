using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Vector2 _posInBoard;
    private EggController _egg;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
    public EggController GetEgg()
    {
        return _egg;
    }
    public void SetEgg(EggController egg)
    {
        _egg = egg;
    }
    public void OnMouseDown()
    {
        GameManager.Instance.PickCell(this);
    }
    public void OnSelected()
    {
        _spriteRenderer.color = new Color(1f, 0.05490196f, 1f);
        transform.Translate(Vector3.up * 0.2f);
    }
}
