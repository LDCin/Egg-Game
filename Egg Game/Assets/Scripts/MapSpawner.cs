using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cellBoardPrefabs;
    [SerializeField] private float _boardEdge;
    [SerializeField] private Cell _cellPrefabs;
    private int _row = 5;
    private int _column = 5;
    private SpriteRenderer _boardSpriteRenderer;
    private Cell[,] _cellBoard;
    private List<CellData> _cellDatas;
    // private List<Cell> _cellList;
    private void Awake()
    {
        _cellDatas = new List<CellData>(Resources.LoadAll<CellData>(GameConfig.CELL_PATH));
        Debug.Log("Load success: " + _cellDatas.Count + " data");
        foreach (var cellData in _cellDatas)
        {
            Debug.Log(cellData.sprite.name);
        }
        // _cellList = new List<Cell>();
        _cellBoard = new Cell[_row, _column];
        _boardSpriteRenderer = _cellBoardPrefabs.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        ShowCellBoard();
    }
    private void ShowCellBoard()
    {
        float usableWidth = _boardSpriteRenderer.bounds.size.x - 1 * _boardEdge;
        float usableHeight = _boardSpriteRenderer.bounds.size.y - 1 * _boardEdge;

        float sizeCellX = usableWidth / _column;
        float sizeCellY = usableHeight / _row;

        float sizeCell = Mathf.Min(sizeCellX, sizeCellY);

        Vector2 firstSpawnPos = new Vector2(
            _boardSpriteRenderer.bounds.min.x + _boardEdge + sizeCell/2 + _boardEdge/15,
            _boardSpriteRenderer.bounds.max.y - _boardEdge - sizeCell/2 - _boardEdge/1.8f);
        Vector2 spawnPos = firstSpawnPos;
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                int index = (i + j) % 2;

                Cell newCell = Instantiate(_cellPrefabs, spawnPos, Quaternion.identity);

                newCell.SetSprite(_cellDatas[index].sprite);

                float spriteWidth = newCell.GetSize();
                float scaleFactor = sizeCell / spriteWidth;
                newCell.transform.localScale = Vector3.one * scaleFactor;

                newCell.gameObject.SetActive(true);
                _cellBoard[i, j] = newCell;

                spawnPos += new Vector2(sizeCell - 0.02f, 0);
            }
            spawnPos = firstSpawnPos - new Vector2(0, (i + 1) * (sizeCell - 0.055f));
        }
    }
}
