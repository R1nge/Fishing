using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using Fish;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<FishSO> fish;
    private FishData[] _data;
    private readonly string _identifier = "inventory";

    private void Awake()
    {
        if (SaveGame.Exists(_identifier))
            Load();
        else
            Save();
    }

    public void Add(FishSO newFish)
    {
        if (fish.Contains(newFish))
        {
            newFish.data.amount++;
        }
        else
        {
            fish.Add(newFish);
            newFish.data.amount++;
        }
    }

    public void Remove(FishSO fishToRemove)
    {
        if (fishToRemove.data.amount > 0)
            fishToRemove.data.amount--;
        else
            fish.Remove(fishToRemove);
    }

    private void Save()
    {
        _data = new FishData[fish.Count];

        for (int i = 0; i < fish.Count; i++)
        {
            _data[i] = fish[i].data;
        }

        SaveGame.Save(_identifier, _data);
    }

    public void SaveCloud() => StartCoroutine(SaveCloud_c());

    private IEnumerator SaveCloud_c()
    {
        print("Inventory saved");
        yield break;
    }

    private void Load()
    {
        _data = SaveGame.Load<FishData[]>(_identifier);

        for (int i = 0; i < fish.Count; i++)
        {
            fish[i].data = _data[i];
        }
    }

    public void LoadCloud() => StartCoroutine(LoadCloud_c());

    private IEnumerator LoadCloud_c()
    {
        print("Inventory loaded");
        yield break;
    }

    private void OnApplicationQuit() => Save();
}