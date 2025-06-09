using UnityEngine;
using System.Collections.Generic;

public class CurrencyRepository
{
    private readonly string SAVE_KEY = nameof(CurrencyRepository);

    // 영속성 -> 게임을 꺼도 데이터 보존
    public void Save(List<CurrencyDTO> dataList)
    {
        CurrencySaveData data = new CurrencySaveData();
        data.DataList = dataList;

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<CurrencyDTO> Load()
    {
        if(!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        CurrencySaveData data = JsonUtility.FromJson<CurrencySaveData>(json);

        return data.DataList;
    }
}
