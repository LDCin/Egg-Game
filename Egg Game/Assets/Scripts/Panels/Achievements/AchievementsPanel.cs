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
        // foreach (var eggData in _eggDatas)
        // {
        //     Debug.Log(eggData.id);
        // }
        _eggDatas.Sort((a, b) => a.id.CompareTo(b.id));
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
                eggAchievement.transform.localScale = new Vector3(1, 1, 1);
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
            Egg egg = Instantiate(_eggAchievementPrefabs);
            egg.SetUp(eggData.id, eggData.sprite, null);
            egg.SetImage(eggData.sprite);
            slot.SetEgg(egg);
            if (eggData.id == 0)
            {
                egg.transform.localScale = new Vector3(0.6f, 0.55f, 0);
            }
            else
            {
                egg.transform.localScale = new Vector3(0.6f, 0.7f, 0);
            }
            egg.transform.SetParent(slot.transform);
            egg.transform.localPosition = Vector3.zero;
            // egg.gameObject.SetActive(false);
            _eggAchievements.Add(slot);
        }
    }
    public void Back()
    {
        Close();
    }
}
