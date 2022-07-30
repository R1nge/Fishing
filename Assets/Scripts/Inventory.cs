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
            //TODO: override if data is not identical
        }
        else
        {
            _data = new FishData[fish.Count];
            for (int i = 0; i < fish.Count; i++)
            {
                _data[i] = fish[i].data;
            }
        }
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

    //TODO: save load

    // private void OnApplicationQuit() => SaveToJson();
}