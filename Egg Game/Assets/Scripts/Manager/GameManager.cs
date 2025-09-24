using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EggSpawner _eggSpawner;
    private Board _board;
    private Cell[,] _cellBoard;
    int[] dx = {-1, 0, 0, 1};
    int[] dy = {0, -1, 1, 0};
    public override void Awake()
    {
        base.Awake();
        _board = Board.Instance;
        _cellBoard = Board.Instance.GetCellBoard();
    }
    private void Start()
    {
        EggSpawner eggSpawner = Instantiate(_eggSpawner, transform);
        _eggSpawner = eggSpawner;
    }
    public bool HasAdjustedCell(Cell cell)
    {
        bool checkHasAdjustedCell = false;

        int x = (int)cell.GetPosInBoard().x;
        int y = (int)cell.GetPosInBoard().y;

        EggController egg = cell.GetEgg();
        if (egg == null)
        {
            Debug.Log("Not Found Egg In Cell!");
            return false;
        }
        int eggLevel = egg.GetLevel();

        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (nx < 0 || ny < 0 || nx >= _cellBoard.GetLength(0) || ny >= _cellBoard.GetLength(1))
            {
                continue;
            }
            Cell adjustedCell = _cellBoard[x + dx[i], y + dy[i]];
            int adjustedEggLevel = adjustedCell.GetEgg().GetLevel();
            if (eggLevel == adjustedEggLevel)
            {
                adjustedCell.OnSelected();
                // PickCell(adjustedCell);
                checkHasAdjustedCell = true;
            }
        }
        if (checkHasAdjustedCell) return true;
        return false;
    }
    public void PickCell(Cell cell)
    {
        if (HasAdjustedCell(cell))
        {
            cell.OnSelected();
        }
        else Debug.Log("Not Found: Adjusted Cell");
    }
}
