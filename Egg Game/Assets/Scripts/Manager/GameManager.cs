using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EggSpawner _eggSpawner;
    private Cell[,] _cellBoard;
    private bool _isSelecting = false;
    int[] dx = { -1, 0, 0, 1 };
    int[] dy = { 0, -1, 1, 0 };
    private HashSet<Vector2> _selectedCellList;
    private List<(Egg, List<Vector2>, float, float)> _mergeEggList;
    [SerializeField] private float _mergeSpeed = 4f;
    private float _fallingTime = 1.0f;

    public override void Awake()
    {
        base.Awake();
        _cellBoard = Board.Instance.GetCellBoard();
        _selectedCellList = new HashSet<Vector2>();
        _mergeEggList = new List<(Egg, List<Vector2>, float, float)>();
    }
    private void Start()
    {
        EggSpawner eggSpawner = Instantiate(_eggSpawner, transform);
        _eggSpawner = eggSpawner;
    }
    // public void ReArrangeBoard()
    // {
    //     Sequence sq = DOTween.Sequence();
    //     int rows = _cellBoard.GetLength(0);
    //     int cols = _cellBoard.GetLength(1);
    //     for (int col = 0; col < cols; col++)
    //     {
    //         Sequence subSq = DOTween.Sequence();
    //         int emptyRow = rows - 1;
    //         for (int row = rows - 1; row >= 0; row--)
    //         {
    //             Egg egg = _cellBoard[row, col].GetEgg();
    //             if (egg != null)
    //             {
    //                 if (row != emptyRow)
    //                 {
    //                     Cell startCell = _cellBoard[row, col];
    //                     Cell endCell = _cellBoard[emptyRow, col];
    //                     startCell.SetEgg(null);
    //                     endCell.SetEgg(egg);
    //                     egg.transform.SetParent(endCell.transform);
    //                     sq.Join(egg.transform.DOMove(endCell.transform.position, _fallingTime));
    //                 }
    //                 emptyRow--;
    //             }
    //         }
    //     }
    //     sq.Play();
    //     Debug.Log("Rearrange Successfully");
    // }
    public void FindPath(Vector2 startPos, Vector2 endPos, List<Vector2> mergePath)
    {
        HashSet<Vector2> visited = new HashSet<Vector2>();
        Queue<Vector2> queue = new Queue<Vector2>();
        Dictionary<Vector2, Vector2> trace = new Dictionary<Vector2, Vector2>();
        queue.Enqueue(startPos);
        trace[startPos] = new Vector2(-1, -1);

        bool foundPath = false;
        while (queue.Count > 0)
        {
            Vector2 pos = queue.Dequeue();
            if (pos == endPos)
            {
                foundPath = true;
                break;
            }

            if (visited.Contains(pos)) continue;
            visited.Add(pos);

            for (int i = 0; i < 4; i++)
            {
                Vector2 adjustedPos = new Vector2(pos.x + dx[i], pos.y + dy[i]);
                if (_selectedCellList.Contains(adjustedPos) && !trace.ContainsKey(adjustedPos))
                {
                    queue.Enqueue(adjustedPos);
                    trace[adjustedPos] = pos;
                }
            }
        }

        if (foundPath)
        {
            Vector2 tempPos = endPos;
            while (trace[tempPos] != new Vector2(-1, -1))
            {
                mergePath.Add(tempPos);
                tempPos = trace[tempPos];
            }
            mergePath.Reverse();
        }
    }
    public void FinishMerge(Egg targetEgg)
    {
        foreach (var selectedCell in _selectedCellList)
        {
            Cell cell = _cellBoard[(int)selectedCell.x, (int)selectedCell.y];
            Egg mergedEgg = cell.GetEgg();
            if (mergedEgg != targetEgg)
            {
                mergedEgg.ReturnToPool();
            }
        }
        targetEgg.LevelUp(_eggSpawner.GetEggPool());
    }
    public void MergeEgg(HashSet<Vector2> selectedCellList, Vector2 targetPos)
    {
        _mergeEggList.Clear();
        float maxMergeTime = 0;
        Sequence sq = DOTween.Sequence();

        foreach (var pos in selectedCellList)
        {
            List<Vector2> mergePath = new List<Vector2>();
            Cell cell = _cellBoard[(int)pos.x, (int)pos.y];
            Egg egg = cell.GetEgg();
            if (egg == null) continue;

            FindPath(pos, targetPos, mergePath);
            if (mergePath.Count == 0) continue;

            float dis = mergePath.Count;
            float mergeTime = dis / _mergeSpeed;
            maxMergeTime = Mathf.Max(maxMergeTime, mergeTime);
            _mergeEggList.Add((egg, mergePath, dis, mergeTime));
        }

        foreach (var mergeEgg in _mergeEggList)
        {
            Sequence subSq = DOTween.Sequence(); List<Vector2> path = mergeEgg.Item2;
            float delayTime = maxMergeTime - mergeEgg.Item4;
            subSq.AppendInterval(delayTime);
            foreach (var pos in path)
            {
                Egg egg = mergeEgg.Item1;
                Cell targetCell = _cellBoard[(int)pos.x, (int)pos.y];
                float timePerStep = mergeEgg.Item4 / path.Count;
                subSq.Append(egg.transform.DOMove(targetCell.transform.position, timePerStep));
            }
            sq.Join(subSq);
        }

        sq.OnComplete(() =>
        {
            var targetCell = _cellBoard[(int)targetPos.x, (int)targetPos.y];
            if (targetCell != null && targetCell.GetEgg() != null)
            {
                FinishMerge(targetCell.GetEgg());
            }
            ClearSelection();
            // ReArrangeBoard();
        });

        sq.Play();
    }


    // public void RemoveEggs()
    // {
    //     List<KeyValuePair<int, int>> spawnPosList = new List<KeyValuePair<int, int>>();
    //     foreach (var pos in _selectedCellList)
    //     {
    //         int x = (int)pos.x;
    //         int y = (int)pos.y;
    //         Cell cell = _cellBoard[x, y];
    //         Egg egg = cell.GetEgg();
    //         egg.ReturnToPool();
    //         cell.OnUnselected();
    //         spawnPosList.Add(new KeyValuePair<int, int>(x, y));
    //     }
    //     _selectedCellList.Clear();
    //     MergeEgg(_selectedCellList, );

    //     foreach (var spawnPos in spawnPosList)
    //     {
    //         _eggSpawner.SpawnEgg(spawnPos.Key, spawnPos.Value);
    //     }
    //     _isSelecting = false;
    // }

    public void ClearSelection()
    {
        foreach (var pos in _selectedCellList)
        {
            int x = (int)pos.x;
            int y = (int)pos.y;
            _cellBoard[x, y].OnUnselected();
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
        Egg egg = cell.GetEgg();
        if (egg == null || egg.GetLevel() != level) return;

        visited[x, y] = true;
        cell.OnSelected();
        _selectedCellList.Add(new Vector2(x, y));

        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];
            DFS(nx, ny, level, visited);
        }
    }

    public void PickCell(Cell cell)
    {
        if (cell.GetEgg().GetID() == _eggSpawner.GetQuantityOfEggType() - 1) return;
        Debug.Log("Picking: " + cell.GetPosInBoard());
        if (_isSelecting)
        {
            Vector2 pos = cell.GetPosInBoard();
            if (_selectedCellList.Contains(pos))
            {
                // RemoveEggs();
                MergeEgg(_selectedCellList, pos);
            }
            else
            {
                ClearSelection();
            }
            return;
        }

        Egg egg = cell.GetEgg();
        if (egg == null)
        {
            Debug.Log("Not Found Egg To Select");
            return;
        }
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

            Egg neighborEgg = _cellBoard[nx, ny].GetEgg();
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
