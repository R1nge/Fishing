using System.Collections.Generic;
using System.IO;
using Fish;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<FishSO> fish;
    private FishData[] _data;

    private void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/Inventory.json"))
        {
            LoadFromJson();
        }
        else
        {
            _data = new FishData[fish.Count];
            for (int i = 0; i < fish.Count; i++)
            {
                _data[i] = fish[i].data; //???
            }
        }

        SaveToJson();
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
            _data = new FishData[fish.Count];
            for (int i = 0; i < fish.Count; i++)
            {
                _data[i] = fish[i].data; //???
            }
        }
    }

    public void Remove(FishSO fishToRemove)
    {
        if (fishToRemove.data.amount > 0)
            fishToRemove.data.amount--;
        else
            fish.Remove(fishToRemove);
    }

    private void LoadFromJson()
    {
        string fileContents = File.ReadAllText(Application.persistentDataPath + "/Inventory.json");
        _data = JsonHelper.FromJson<FishData>(fileContents);
        for (int i = 0; i < fish.Count; i++)
        {
            fish[i].data = _data[i];
        }
    }

    private void SaveToJson() =>
        File.WriteAllText(Application.persistentDataPath + "/Inventory.json", JsonHelper.ToJson(_data, true));

    private void OnApplicationQuit() => SaveToJson();
}