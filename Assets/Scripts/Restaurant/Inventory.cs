using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;

namespace Restaurant
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<IngredientSo> fish;
        private Dictionary<string, Data> _data;

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
            _data = new Dictionary<string, Data>(fish.Count);

            for (int i = 0; i < fish.Count; i++)
            {
                _data[fish[i].title] = fish[i].data;
            }

            SaveGame.Save(_identifier, _data);
        }

        private void Load()
        {
            _data = SaveGame.Load<Dictionary<string, Data>>(_identifier);

            for (int i = 0; i < fish.Count; i++)
            {
                if (_data.TryGetValue(fish[i].title, out Data data))
                {
                    fish[i].data = data;
                }
            }
        }
    }
}