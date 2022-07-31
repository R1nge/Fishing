using System;
using System.Collections;
using System.Text.RegularExpressions;
using BayatGames.SaveGameFree;
using DanielLochner.Assets.SimpleScrollSnap;
using FishingRod;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private FishingRodSo[] so;
    private RodData[] _data;
    private readonly string _identifier = "rods";
    private SimpleScrollSnap _scroll;

    private void Awake()
    {
        _scroll = FindObjectOfType<SimpleScrollSnap>();
        if (SaveGame.Exists(_identifier))
            Load();
        else
            Save();
    }

    private void Save()
    {
        _data = new RodData[so.Length];

        for (int i = 0; i < so.Length; i++)
        {
            _data[i] = so[i].data;
        }

        SaveGame.Save(_identifier, _data);
    }

    public void SaveCloud() => StartCoroutine(SaveCloud_c());

    private IEnumerator SaveCloud_c()
    {
        print("Rods saved");
        yield break;
    }

    private void Load()
    {
        _data = SaveGame.Load<RodData[]>(_identifier);

        for (int i = 0; i < so.Length; i++)
        {
            so[i].data = _data[i];
            so[i].OnSpriteChanged += Save;
        }

        ShowSelected();
    }

    public void LoadCloud() => StartCoroutine(LoadCloud_c());

    private IEnumerator LoadCloud_c()
    {
        print("Rods loaded");
        yield break;
    }

    private void ShowSelected()
    {
        if (_scroll == null) return;
        var str = Regex.Replace(so[0].data.rodTitle, "\\D+", String.Empty);
        if (str.Length > 0)
        {
            _scroll.StartingPanel = int.Parse(str) - 1;
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