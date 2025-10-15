using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    //  private EggPool _eggPoolPrefab;
    [SerializeField] private EggPool _eggPool;

    private int _existEgg = 25;
    private Cell[,] _cellBoard;
    private int _eggIDLimit = 4;
    private void Awake()
    {
        EggPool eggPool = Instantiate(_eggPool, transform);
        _eggPool = eggPool;
    }
    private void Start()
    {
        _cellBoard = Board.Instance.GetCellBoard();
        _existEgg = Board.Instance.GetBoardSize();
        for (int i = 0; i < _existEgg; i++)
        {
            SpawnEgg();
        }
    }
    public int GetQuantityOfEggType()
    {
        return _eggPool.GetEggDataSize();
    }
    public EggPool GetEggPool()
    {
        return _eggPool;
    }
    private Cell GetEmptySlotInBoard()
    {
        for (int i = 0; i < _cellBoard.GetLength(0); i++)
        {
            for (int j = 0; j < _cellBoard.GetLength(1); j++)
            {
                if (_cellBoard[i, j].transform.childCount > 0)
                {
                    continue;
                }
                return _cellBoard[i, j];
            }
        }
        return null;
    }
    private void SpawnEgg()
    {
        int randomId = _eggPool.GetRandomEggId(_eggIDLimit);
        Egg egg = _eggPool.GetEgg(randomId);
        // if (egg == null)
        // {
        //     return;
        // }
        Cell cell = GetEmptySlotInBoard();
        if (cell != null)
        {
            egg.transform.SetParent(cell.transform);
            egg.transform.localPosition = Vector3.zero;
            cell.SetEgg(egg);
        }
        egg.gameObject.SetActive(true);
    }
    public Egg SpawnEgg(int x, int y)
    {
        int randomId = _eggPool.GetRandomEggId(_eggIDLimit);
        Egg egg = _eggPool.GetEgg(randomId);
        if (egg == null) return null;

        Cell cell = _cellBoard[x, y];
        if (cell == null) return null;

        egg.transform.SetParent(cell.transform);
        Vector3 startLocalPos = new Vector3(0, Board.Instance.GetSpriteSizeY(), 0);
        egg.transform.localPosition = startLocalPos;
        cell.SetEgg(egg);

        egg.gameObject.SetActive(true);
        return egg;
    }
}
