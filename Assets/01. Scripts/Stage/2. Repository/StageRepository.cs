using System.Collections.Generic;
using UnityEngine;

public class StageRepository
{
    private readonly string SAVE_KEY;

    public StageRepository()
    {
        if(AccountManager.Instance != null && !string.IsNullOrEmpty(AccountManager.Instance.UserId))
        {
            SAVE_KEY = $"{AccountManager.Instance.UserId}{nameof(StageRepository)}";
        }
        else
        {
            Debug.LogError("AccountManager가 없습니다.");
            SAVE_KEY = nameof(StageRepository);
        }
    }

    public void Save(StageDTO stageDto)
    {
        StageSaveData data = new StageSaveData(stageDto);

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);

        Debug.Log($"Save : {json}");
    }

    public StageSaveData Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        StageSaveData data = JsonUtility.FromJson<StageSaveData>(json);

        return data; //.ConvertAll<AchievementDTO>(data => new AchievementDTO(data.ID, data.CurrentValue, data.IsRewardClaimed));
    }
}
