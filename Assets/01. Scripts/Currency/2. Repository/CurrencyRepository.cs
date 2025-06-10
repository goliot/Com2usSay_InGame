using UnityEngine;
using System.Collections.Generic;

public class CurrencyRepository
{
    private readonly string SAVE_KEY = nameof(CurrencyRepository);

    // 영속성 -> 게임을 꺼도 데이터 보존
    // Save
    public void Save(List<CurrencyDTO> dataList)
    {
        CurrencySaveDataList datas = new CurrencySaveDataList();
        datas.DataList = dataList.ConvertAll(data => new CurrencySaveData
        {
            Type = data.Type,
            Value = data.Value
        });

        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    // Load
    public List<CurrencyDTO> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        CurrencySaveDataList datas = JsonUtility.FromJson<CurrencySaveDataList>(json);

        return datas.DataList.ConvertAll<CurrencyDTO>(data => new CurrencyDTO(data.Type, data.Value));
    }
}
