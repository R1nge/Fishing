using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using Restaurant;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Use dictionary?
    public List<IngredientSo> fish;
    private Data[] _data;
    private readonly string _identifier = "inventory";

    private void Awake()
    {
        if (SaveGame.Exists(_identifier))
            Load();
        else
            Save();
    }

    public void Add(IngredientSo newFish)
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
        
        Save();
    }

    public void Remove(IngredientSo fishToRemove)
    {
        if (fishToRemove.data.amount > 0)
            fishToRemove.data.amount--;
        else
            fish.Remove(fishToRemove);
    }

    private void Save()
    {
        _data = new Data[fish.Count];

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
        _data = SaveGame.Load<Data[]>(_identifier);

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

    private void OnDisable() => Save();

    private void OnApplicationQuit() => Save();
}