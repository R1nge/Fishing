using BayatGames.SaveGameFree;
using FishingRod;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string identifier = "rods";
    [SerializeField] private FishingRodSo[] so;
    private RodData[] _data;

    private void Awake()
    {
        if (SaveGame.Exists(identifier))
        {
            Load();
        }
        else
        {
            Save();
        }
    }

    private void Save()
    {
        _data = new RodData[so.Length];
        for (int i = 0; i < so.Length; i++)
        {
            _data[i] = so[i].data; 
        }

        SaveGame.Save(identifier, _data);
    }

    private void Load()
    {
        _data = SaveGame.Load<RodData[]>(identifier);

        for (int i = 0; i < so.Length; i++)
        {
            so[i].data = _data[i];
            so[i].OnSpriteChanged += Save;
        }
    }

    private void OnApplicationQuit() => Save();

    private void OnDestroy()
    {
        for (int i = 0; i < so.Length; i++)
        {
            so[i].OnSpriteChanged -= Save;
        }
    }
}