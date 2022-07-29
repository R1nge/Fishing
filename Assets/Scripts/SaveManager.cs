using System.IO;
using FishingRod;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private FishingRodSo[] so;
    private RodData[] _data;
#if !UNITY_EDITOR
    private void Awake()
    {
        
        if (File.Exists(Application.persistentDataPath + "/Rod.json"))
        {
            LoadFromJson();
        }
        else
        {
            _data = new RodData[so.Length];
            for (int i = 0; i < so.Length; i++)
            {
                _data[i] = so[i].data; //???
            }
        }

        for (int i = 0; i < so.Length; i++)
        {
            so[i].OnSpriteChanged += SaveToJson;
        }

        SaveToJson();
    }

    private void LoadFromJson()
    {
        string fileContents = File.ReadAllText(Application.persistentDataPath + "/Rod.json");
        _data = JsonHelper.FromJson<RodData>(fileContents);
        for (int i = 0; i < so.Length; i++)
        {
            so[i].data = _data[i];
        }
    }

    private void SaveToJson() =>
        File.WriteAllText(Application.persistentDataPath + "/Rod.json", JsonHelper.ToJson(_data, true));

    private void OnApplicationQuit() => SaveToJson();


    private void OnDestroy()
    {
        for (int i = 0; i < so.Length; i++)
        {
            so[i].OnSpriteChanged -= SaveToJson;
        }
    }
#endif
}