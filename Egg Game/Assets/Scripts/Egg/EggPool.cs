using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPool : MonoBehaviour
{
    private EggController _eggPrefabs;
    private List<EggData> eggDatas;
    private List<EggController> eggs;
    private int poolSize = 25;
    private int quantityForEachEgg;

    private void Awake()
    {
        quantityForEachEgg = poolSize / 2;
        eggDatas = new List<EggData>(Resources.LoadAll<EggData>(GameConfig.EGG_PATH));
        InitEggList();
    }
    private void InitEggList()
    {
        eggs = new List<EggController>();

        foreach (var eggData in eggDatas)
        {
            EggController egg = Instantiate(_eggPrefabs);
            for (int i = 0; i < quantityForEachEgg; i++)
            {
                egg.SetUp(eggData.id, eggData.sprite, eggData.eggAnimation);
            }
            eggs.Add(egg);
        }
    }
    private EggController HasAvailableEgg()
    {
        foreach (var egg in eggs)
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
        foreach (var eggData in eggDatas)
        {
            if (eggData.id == id)
            {
                egg = Instantiate(_eggPrefabs);
                egg.SetUp(eggData.id, eggData.sprite, eggData.eggAnimation);
                eggs.Add(egg);
                break;
            }
        }
        return egg;
    }
}
