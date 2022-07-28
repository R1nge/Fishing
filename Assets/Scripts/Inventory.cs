using System.Collections.Generic;
using Fish;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "SO/Inventory")]
public class Inventory : ScriptableObject
{
    public List<FishStats> fish;

    public void Add(FishStats newFish) => fish.Add(newFish);

    public void Remove(FishStats fishToRemove) => fish.Remove(fishToRemove);
}