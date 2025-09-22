using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPool : MonoBehaviour
{
    private EggController _eggPrefabs;
    private List<EggData> _eggDatas;
    private List<EggController> _eggs;
    private int _poolSize = 25;
    private int _quantityForEachEgg;

    private void Awake()
    {
        _quantityForEachEgg = _poolSize / 2;
        _eggDatas = new List<EggData>(Resources.LoadAll<EggData>(GameConfig.EGG_PATH));
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
            EggController egg = Instantiate(_eggPrefabs);
            for (int i = 0; i < _quantityForEachEgg; i++)
            {
                egg.SetUp(eggData.id, eggData.sprite, eggData.eggAnimation);
            }
            _eggs.Add(egg);
        }
    }
    private EggController HasAvailableEgg()
    {
        foreach (var egg in _eggs)
        {
            if (!egg.gameObject.activeInHierarchy)
            {
                Debug.Log("Found Available Egg");
                return egg;
            }
        }
        return null;
    }
    private EggController GetEgg(int id)
    {
        EggController egg = HasAvailableEgg();
        if (HasAvailableEgg() != null)
        {
            return egg;
        }
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
}
