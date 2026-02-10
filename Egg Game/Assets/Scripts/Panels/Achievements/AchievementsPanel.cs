using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsPanel : Panel
{
    [Header("Prefabs")]
    [SerializeField] private Egg _eggAchievementPrefab;
    [SerializeField] private Cell _unlockedCellPrefab;
    [SerializeField] private Cell _notUnlockedCellPrefab;

    [Header("UI")]
    [SerializeField] private Transform _achievementsScrollViewContent;

    private List<EggData> _eggDatas;
    private readonly List<Cell> _cells = new();

    private void Start()
    {
        LoadData();
        SpawnAchievements();
    }
    private void LoadData()
    {
        _eggDatas = new List<EggData>(
            Resources.LoadAll<EggData>(GameConfig.EGG_PATH)
        );

        _eggDatas.Sort((a, b) => a.id.CompareTo(b.id));
    }
    private void SpawnAchievements()
    {
        ClearOldCells();

        foreach (var eggData in _eggDatas)
        {
            bool isUnlocked = eggData.id <= GameConfig.MAX_EGG_LEVEL_HIGH_SCORE;

            Cell cell = Instantiate(isUnlocked ? _unlockedCellPrefab : _notUnlockedCellPrefab, _achievementsScrollViewContent
            );

            _cells.Add(cell);

            if (!isUnlocked)
                continue;

            SpawnEgg(cell, eggData);
        }
    }
    private void SpawnEgg(Cell cell, EggData eggData)
    {
        Egg egg = Instantiate(_eggAchievementPrefab);
        egg.SetUp(eggData.id, eggData.sprite, null);
        egg.SetImage(eggData.sprite);

        cell.SetEgg(egg);

        RectTransform eggRect = egg.GetComponent<RectTransform>();
        eggRect.SetParent(cell.transform, false);
        eggRect.anchoredPosition = Vector2.zero;
        eggRect.localScale = Vector3.one;

        eggRect.sizeDelta = eggData.uiSize;

        Image img = egg.GetComponent<Image>();
        if (img != null)
            img.preserveAspect = true;
    }
    private void ClearOldCells()
    {
        foreach (var cell in _cells)
        {
            if (cell != null)
                Destroy(cell.gameObject);
        }
        _cells.Clear();
    }

    public void Back()
    {
        SoundManager.Instance.PlayClickSound();
        Close();
    }
}
