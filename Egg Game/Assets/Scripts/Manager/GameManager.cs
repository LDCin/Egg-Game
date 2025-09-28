using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EggSpawner _eggSpawner;
    private Board _board;
    private Cell[,] _cellBoard;
    private bool _isSelecting = false;
    int[] dx = { -1, 0, 0, 1 };
    int[] dy = { 0, -1, 1, 0 };
    private List<Cell> _selectedCellList;

    public override void Awake()
    {
        base.Awake();
        _board = Board.Instance;
        _cellBoard = Board.Instance.GetCellBoard();
        _selectedCellList = new List<Cell>();
    }
    private void Start()
    {
        EggSpawner eggSpawner = Instantiate(_eggSpawner, transform);
        _eggSpawner = eggSpawner;
    }
    public void RemoveEggs()
    {
        List<KeyValuePair<int, int>> spawnPosList = new List<KeyValuePair<int, int>>();
        foreach (var cell in _selectedCellList)
        {
            EggController egg = cell.GetEgg();
            egg.ReturnToPool();
            cell.OnUnselected();
            spawnPosList.Add(new KeyValuePair<int, int>((int)cell.GetPosInBoard().x, (int)cell.GetPosInBoard().y));
        }
        _selectedCellList.Clear();

        foreach (var spawnPos in spawnPosList)
        {
            _eggSpawner.SpawnEgg(spawnPos.Key, spawnPos.Value);
        }
        _isSelecting = false;
    }
    public void ClearSelection()
    {
        foreach (var cell in _selectedCellList)
        {
            cell.OnUnselected();
        }
        _selectedCellList.Clear();
        _isSelecting = false;
    }
    private void DFS(int x, int y, int level, bool[,] visited)
    {
        if (x < 0 || y < 0 || x >= _cellBoard.GetLength(0) || y >= _cellBoard.GetLength(1))
        {
            return;
        }
        if (visited[x, y]) return;

        Cell cell = _cellBoard[x, y];
        EggController egg = cell.GetEgg();
        if (egg == null || egg.GetLevel() != level) return;

        visited[x, y] = true;
        cell.OnSelected();
        _selectedCellList.Add(cell);

        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];
            DFS(nx, ny, level, visited);
        }
    }
    public void PickCell(Cell cell)
    {
        if (_isSelecting)
        {
            if (_selectedCellList.Contains(cell))
            {
                RemoveEggs();
            }
            else
            {
                ClearSelection();
            }
            return;
        }

        EggController egg = cell.GetEgg();

        int eggLevel = egg.GetLevel();
        int x = (int)cell.GetPosInBoard().x;
        int y = (int)cell.GetPosInBoard().y;

        bool hasSameNeighbor = false;
        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (nx < 0 || ny < 0 || nx >= _cellBoard.GetLength(0) || ny >= _cellBoard.GetLength(1))
                continue;

            EggController neighborEgg = _cellBoard[nx, ny].GetEgg();
            if (neighborEgg != null && neighborEgg.GetLevel() == eggLevel)
            {
                hasSameNeighbor = true;
                break;
            }
        }

        if (hasSameNeighbor)
        {
            _isSelecting = true;
            bool[,] visited = new bool[_cellBoard.GetLength(0), _cellBoard.GetLength(1)];
            DFS(x, y, eggLevel, visited);
        }
    }

}

