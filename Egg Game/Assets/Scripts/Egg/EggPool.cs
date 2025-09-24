using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPool : MonoBehaviour
{
    // SIZE
    private int _poolSize;
    [SerializeField] private int _quantityForEachEgg;

    // LIST EGG
    [SerializeField] private EggController _eggPrefabs;
    private List<EggData> _eggDatas;
    private List<EggController> _eggs;

    private void Awake()
    {
        _eggDatas = new List<EggData>(Resources.LoadAll<EggData>(GameConfig.EGG_PATH));
        _poolSize = _eggDatas.Count * _quantityForEachEgg;
        InitEggList();
    }
    public int GetPoolSize()
    {
        return _poolSize;
    }
    private void InitEggList()
    {
        _eggs = new List<EggController>();

        foreach (var eggData in _eggDatas)
        {
            for (int i = 0; i < _quantityForEachEgg; i++)
            {
                EggController egg = Instantiate(_eggPrefabs);
                egg.SetUp(eggData.id, eggData.sprite, eggData.eggAnimation);
                egg.gameObject.SetActive(false);
                _eggs.Add(egg);
            }
        }
    }
    private EggController HasAvailableEgg(int id)
    {
        foreach (var egg in _eggs)
        {
            if (!egg.gameObject.activeInHierarchy && egg.GetID() == id)
            {
                Debug.Log("Found Available Egg");
                return egg;
            }
        }
        return null;
    }
    public EggController GetEgg(int id)
    {
        EggController egg = HasAvailableEgg(id);
        if (egg != null)
        {
            return egg;
        }
        Debug.LogError($"EggPool: Không tìm thấy EggData với id = {id}");
        foreach (var eggData in _eggDatas)
        {
            if (eggData.id == id)
            {
                egg = Instantiate(_eggPrefabs);
                egg.SetUp(eggData.id, eggData.sprite, eggData.eggAnimation);
                _eggs.Add(egg);
                break;
            }
        }
        return egg;
    }
    public int GetRandomEggId()
    {
        if (_eggDatas.Count == 0) return -1;
        int index = Random.Range(0, _eggDatas.Count);
        return _eggDatas[index].id;
    }

}
