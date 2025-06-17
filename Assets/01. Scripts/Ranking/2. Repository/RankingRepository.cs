using System.Collections.Generic;
using UnityEngine;

public class RankingRepository
{
    private readonly string SAVE_KEY = nameof(RankingRepository);

    public void Save(List<RankingDTO> rankings)
    {
        RankingSaveDataList datas = new RankingSaveDataList();
        datas.DataList = rankings.ConvertAll(ranking => new RankingSaveData(ranking));

        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<RankingSaveData> Load()
    {
        List<RankingSaveData> mockDatas = new List<RankingSaveData>();
        for(int i=0; i<100; ++i)
        {
            mockDatas.Add(new RankingSaveData($"{GenerateRandomID()}", Random.Range(0, 500)));
        }
        return mockDatas;

        if(!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        RankingSaveDataList datas = JsonUtility.FromJson<RankingSaveDataList>(json);

        return datas.DataList;
    }

    public RankingSaveData GetMyRanking()
    {
        string id;
        if (AccountManager.Instance != null)
        {
            id = AccountManager.Instance.UserId;
        }
        else
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        RankingSaveDataList datas = JsonUtility.FromJson<RankingSaveDataList>(json);

        RankingSaveData myData = datas.DataList.Find((item) => item.Id == id);

        return myData;
    }

    private string GenerateRandomID(int length = 8)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] id = new char[length];
        for (int i = 0; i < length; i++)
        {
            id[i] = chars[Random.Range(0, chars.Length)];
        }
        return new string(id);
    }
}