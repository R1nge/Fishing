using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using Other;

namespace Restaurant
{
    public class Inventory : Singleton<Inventory>
    {
        public List<IngredientSo> fish;
        private Dictionary<string, Data> _data;
        private readonly string _identifier = "inventory";

        public override void Awake()
        {
            base.Awake();
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

        public void SaveCloud() => StartCoroutine(SaveCloud_c());

        private IEnumerator SaveCloud_c()
        {
            print("Inventory saved");
            yield break;
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

        public void LoadCloud() => StartCoroutine(LoadCloud_c());

        private IEnumerator LoadCloud_c()
        {
            print("Inventory loaded");
            yield break;
        }

        private void OnDisable() => Save();

        private void OnApplicationQuit() => Save();
    }
}