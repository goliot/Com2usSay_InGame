using System.Collections.Generic;
using UnityEngine;

public class AchievementRepository
{
    private readonly string SAVE_KEY = nameof(AchievementRepository);

    public void Save(List<AchievementDTO> achievements)
    {
        AchievementSaveDataList datas = new AchievementSaveDataList();
        datas.DataList = achievements.ConvertAll(achievement => new AchievementSaveData
        {
            ID = achievement.ID,
            CurrentValue = achievement.CurrentValue,
            IsRewardClaimed = achievement.IsRewardClaimed,
        });

        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY, json);

        //Debug.Log($"Save : {json}");
    }

    public List<AchievementSaveData> Load()
    {
        if(!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        AchievementSaveDataList datas = JsonUtility.FromJson<AchievementSaveDataList>(json);

        return datas.DataList;//.ConvertAll<AchievementDTO>(data => new AchievementDTO(data.ID, data.CurrentValue, data.IsRewardClaimed));
    }
}
