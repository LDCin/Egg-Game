using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Singleton<Board>
{
    [SerializeField] private GameObject _cellBoardPrefabs;
    [SerializeField] private Cell _cellPrefabs;
    [SerializeField] private float _boardEdge;
    [SerializeField] private int _row = 5;
    [SerializeField] private int _column = 5;
    private SpriteRenderer _boardSpriteRenderer;
    private Cell[,] _cellBoard;
    private List<CellData> _cellDatas;
    public override void Awake()
    {
        base.Awake();
        _cellDatas = new List<CellData>(Resources.LoadAll<CellData>(GameConfig.CELL_PATH));
        _cellBoard = new Cell[_row, _column];
        _boardSpriteRenderer = _cellBoardPrefabs.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        InitBoard();
    }
    public int GetBoardSize()
    {
        return _row * _column;
    }
    public Cell[,] GetCellBoard()
    {
        return _cellBoard;
    }
    public float GetSpriteSizeY()
    {
        return _boardSpriteRenderer.bounds.size.y;
    }
    
    [ContextMenu("Init Board")]
    private void InitBoard()
    {
        float usableSize = _boardSpriteRenderer.bounds.size.x - 2 * _boardEdge;
        float sizeCell = usableSize / _column;
        Vector2 boardCenter = _boardSpriteRenderer.bounds.center;
        float startX = boardCenter.x - (sizeCell * (_column - 1) / 2f);
        float startY = boardCenter.y + (sizeCell * (_row - 1) / 2f);
        Vector2 firstSpawnPos = new Vector2(startX + 0.0444444f / 2, startY - 0.2444445f / 2);
        Vector2 spawnPos = firstSpawnPos;

        transform.localScale = new Vector3(0.439f, 0.422f, 0);
        transform.position = new Vector3(0, -1.238f, 0);

        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                int index = (i + j) % 2;

                // Cell newCell = Instantiate(_cellPrefabs, spawnPos, Quaternion.identity, _cellBoardPrefabs.transform);
                Cell newCell = Instantiate(_cellPrefabs, spawnPos, Quaternion.identity);


                newCell.SetSprite(_cellDatas[index].sprite);
                newCell.SetSortingOrder(i);

                float spriteWidth = newCell.GetSize();
                float scaleFactor = sizeCell / spriteWidth;
                newCell.transform.localScale = Vector3.one * scaleFactor;

                newCell.SetPosInBoard(i, j);
                _cellBoard[i, j] = newCell;

                // spawnPos += new Vector2(sizeCell - 0.02f, 0);
                spawnPos += new Vector2(sizeCell - 0.014f, 0);
                // spawnPos += new Vector2(sizeCell, 0);
            }
            spawnPos = firstSpawnPos - new Vector2(0, (i + 1) * (sizeCell - 0.055f));
            // spawnPos = firstSpawnPos - new Vector2(0, (i + 1) * (sizeCell));
        }
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                _cellBoard[i, j].transform.SetParent(transform);
            }
        }
    }

}
