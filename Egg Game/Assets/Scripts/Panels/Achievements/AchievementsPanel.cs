using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AchievementsPanel : Panel
{
    [SerializeField] Egg _eggAchievementPrefabs;
    [SerializeField] Cell _unlockedCellPrefabs;
    [SerializeField] Cell _notUnlockedCellPrefabs;
    [SerializeField] GameObject _achievementsScrollViewContent;
    private List<EggData> _eggDatas;
    private List<Cell> _eggAchievements;
    private bool _isFirst = true;
    private void OnEnable()
    {
        if (!_isFirst)
        {
            LoadAchievement();
        }
    }
    private void Start()
    {
        _eggDatas = new List<EggData>(Resources.LoadAll<EggData>(GameConfig.EGG_PATH));
        _eggAchievements = new List<Cell>();
        SpawnAchievement();
        LoadAchievement();
        _isFirst = false;
    }
    private void LoadAchievement()
    {
        List<Cell> notUnlockedCells = new List<Cell>();
        foreach (var eggAchievement in _eggAchievements)
        {
            if (eggAchievement.GetEgg().GetLevel() <= GameConfig.MAX_EGG_LEVEL_HIGH_SCORE)
            {
                eggAchievement.transform.SetParent(_achievementsScrollViewContent.transform);
            }
            else
            {
                Cell notUnlockedCell = Instantiate(_notUnlockedCellPrefabs, _achievementsScrollViewContent.transform);
                notUnlockedCells.Add(notUnlockedCell);
            }
        }

        foreach (var notUnlockedCell in notUnlockedCells)
        {
            _eggAchievements.Add(notUnlockedCell);
        }
    }
    private void SpawnAchievement()
    {
        foreach (Cell eggAchievement in _eggAchievements)
        {
            Destroy(eggAchievement.gameObject);
        }
        foreach (var eggData in _eggDatas)
        {
            Cell slot = Instantiate(_unlockedCellPrefabs);
            Egg egg = Instantiate(_eggAchievementPrefabs, slot.transform);
            slot.SetEgg(egg);
            egg.SetUp(eggData.id, eggData.sprite, null);
            egg.transform.localPosition = Vector3.zero;
            egg.gameObject.SetActive(false);
            _eggAchievements.Add(slot);
        }
    }
    public void Back()
    {
        Close();
    }
}
