using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPool : MonoBehaviour
{
    // SIZE
    private int _poolSize;
    [SerializeField] private int _quantityForEachEgg;

    // LIST EGG
    [SerializeField] private Egg _eggPrefabs;
    private List<EggData> _eggDatas;
    private List<Egg> _eggs;

    private void Awake()
    {
        _eggDatas = new List<EggData>(Resources.LoadAll<EggData>(GameConfig.EGG_PATH));
        _eggDatas.Sort((a, b) => a.id.CompareTo(b.id));
        _poolSize = _eggDatas.Count * _quantityForEachEgg;
        InitEggList();
        foreach (var eggData in _eggDatas)
        {
            Debug.Log(eggData.id);
            Debug.Log(eggData.name);
        }
    }
    public int GetEggDataSize()
    {
        return _eggDatas.Count;
    }
    public int GetPoolSize()
    {
        return _poolSize;
    }
    public List<EggData> GetEggDatas()
    {
        return _eggDatas;
    }
    private void InitEggList()
    {
        _eggs = new List<Egg>();

        foreach (var eggData in _eggDatas)
        {
            for (int i = 0; i < _quantityForEachEgg; i++)
            {
                Egg egg = Instantiate(_eggPrefabs);
                egg.SetUp(eggData.id, eggData.sprite, eggData.eggAnimation);
                egg.gameObject.SetActive(false);
                _eggs.Add(egg);
            }
        }
    }
    private Egg HasAvailableEgg(int id)
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
    public Egg GetEgg(int id)
    {
        Egg egg = HasAvailableEgg(id);
        if (egg != null)
        {
            return egg;
        }
        Debug.Log($"EggPool: Not Found EggData with id = {id}");
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
    public int GetRandomEggId(int maxSpawnID)
    {
        if (_eggDatas.Count == 0) return -1;

        // List<int> availableIDs = new List<int>();
        // foreach (var data in _eggDatas)
        // {
        //     if (data.id < maxSpawnID)
        //         availableIDs.Add(data.id);
        // }

        // if (availableIDs.Count == 0)
        // {
        //     Debug.LogError("Không có EggData nào hợp lệ để spawn!");
        //     return -1;
        // }

        // int index = Random.Range(0, availableIDs.Count);
        // return availableIDs[index];
        return Random.Range(0, maxSpawnID);
    }

}
