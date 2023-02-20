using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BayatGames.SaveGameFree;
using DanielLochner.Assets.SimpleScrollSnap;
using Other;
using UnityEngine;

namespace FishingRod
{
    public class SaveManager : Singleton<SaveManager>
    {
        [SerializeField] private FishingRodSo[] so;
        private Dictionary<string, RodData> _rods;
        private readonly string _identifier = "rods";
        private SimpleScrollSnap _scroll;

        public override void Awake()
        {
            base.Awake();
            _scroll = FindObjectOfType<SimpleScrollSnap>();
            if (SaveGame.Exists(_identifier))
                Load();
            else
                Save();
        }

        private void Save()
        {
            _rods = new Dictionary<string, RodData>(so.Length);

            for (int i = 0; i < so.Length; i++)
            {
                _rods[so[i].data.rodTitle] = so[i].data;
            }

            SaveGame.Save(_identifier, _rods);
        }

        public void SaveCloud() => StartCoroutine(SaveCloud_c());

        private IEnumerator SaveCloud_c()
        {
            print("Rods saved");
            yield break;
        }

        private void Load()
        {
            _rods = SaveGame.Load<Dictionary<string, RodData>>(_identifier);

            for (int i = 0; i < so.Length; i++)
            {
                if (_rods.TryGetValue(so[i].data.rodTitle, out RodData data))
                {
                    so[i].data = data;
                    so[i].OnSpriteChanged += Save;
                }
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
}